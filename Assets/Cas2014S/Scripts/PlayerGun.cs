using UnityEngine;
using System.Collections;

public enum PlayerGunType
{
	Pistol,
	Shotgun,
	GranadeLauncher,

	Max,
}

public abstract class PlayerGun : GunBase
{
	public abstract PlayerGunType Type { get; }

	public float standRecoil = 2.0f;
	public float runRecoil = 4.0f;
	public float crouchRecoil = 1.0f;
	
	// 次弾発射までの間隔
	public float intervalTime = 0.1f;
	
	// 0.0で次弾発射可能
	float intervalRemaining = 0.0f;
	
	public float reloadSpeed = 1.0f;
	
	// 1.0でリロード完了
	float reloadDuration = 1.0f;
	
	public int magazineSize = 10;
	
	public int magazineRemaining{ get; protected set; }

	// Use this for initialization
	void Start()
	{
		magazineRemaining = magazineSize;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(IsReloading())
		{
			reloadDuration += GetReloadSpeed() * Time.deltaTime;
			if(reloadDuration >= 1.0f)
			{
				reloadDuration = 1.0f;
				magazineRemaining = magazineSize;
				//Debug.Log("Reload complete");
			}
		}
		else if(magazineRemaining == 0)
		{
			Reload ();
		}
		
		if(IsInterval())
		{
			intervalRemaining -= Time.deltaTime;
			if(intervalRemaining <= 0.0f)
			{
				intervalRemaining = 0.0f;
			}
		}
	}

	public float GetReloadSpeed()
	{
		var result = reloadSpeed;
		var components = GetComponents<GS_ReloadUp>();
		foreach(var component in components)
		{
			result *= component.reloadSpeedScale;
		}
		return result;
	}
	
	public void Shoot()
	{
		if(magazineRemaining == 0)
		{
			return;
		}
		
		if(IsInterval())
		{
			return;
		}
		
		if(IsReloading())
		{
			return;
		}

		var targetPosition = GetCameraTarget();

		targetPosition = ApplyScatter(targetPosition, GetRecoil());
		
		Fire (targetPosition);
		
		intervalRemaining = intervalTime;
	}

	protected Vector3 ApplyScatter(Vector3 targetPosition, float scatterAngle)
	{
		var centerDirection = targetPosition - muzzle.transform.position;
		var length = centerDirection.magnitude;
		centerDirection.Normalize();
		
		var axis = Vector3.Cross(centerDirection, Vector3.up);
		axis.Normalize();

		// 銃口を原点とみなす
		var targetPoint = centerDirection;
		
		// 中心軸から5度ずらす
		targetPoint = Quaternion.AngleAxis(Random.value * scatterAngle, axis) * targetPoint;
		
		// 中心軸を中心に円状に分布
		targetPoint = Quaternion.AngleAxis(Random.value * 360.0f, centerDirection) * targetPoint;

		return targetPoint * length + muzzle.transform.position;
	}

	public float GetRecoil()
	{
		var playerController = GetPlayerController();
		var run = playerController.run;
		var crouch = playerController.crouch;

		if(run > 0.0f)
		{
			return Mathf.Lerp(standRecoil, runRecoil, run);
		}

		if(crouch > 0.0f)
		{
			return Mathf.Lerp(standRecoil, crouchRecoil, crouch);
		}

		return standRecoil;
	}

	PlayerController GetPlayerController()
	{
		var player = GameObject.FindWithTag("Player");
		return player.GetComponent<PlayerController>();
	}
	
	public void Reload()
	{
		if(magazineRemaining == magazineSize)
		{
			return;
		}
		
		if(IsReloading())
		{
			return;
		}
		
		//Debug.Log("Reloading...");
		
		reloadDuration = 0.0f;
	}
	
	public bool IsReloading()
	{
		return reloadDuration < 1.0f;
	}
	
	bool IsInterval()
	{
		return intervalRemaining > 0.0f;
	}
	
	protected virtual void Fire(Vector3 targetPosition)
	{
	}
}
