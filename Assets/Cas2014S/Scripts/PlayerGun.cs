using UnityEngine;
using System.Collections;

public enum PlayerGunType
{
	Pistol,
	Shotgun,
	GranadeLauncher,
	FlameThrower,
	Shield,
	Pachinko,
	Magnum,
	AssaultRifle,
	RocketLauncher,

	Max,
}

public class PlayerGun : GunBase
{
	public PlayerGunType Type;

	public string Name;

	public string Description;
	
	public int rare;

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

	public bool fullAuto;

	protected bool isPause;
	
	// Use this for initialization
	void Start()
	{
		magazineRemaining = magazineSize;
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		if(isPause)
		{
			return;
		}

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
		
		intervalRemaining = GetIntervalTime();
	}

	float GetIntervalTime()
	{
		var scale = 1.0f;
		var components = GetComponents<GS_FastInterval>();
		foreach(var component in components)
		{
			scale *= component.intervalScale;
		}
		return intervalTime * scale;
	}

	public void ShootFullAuto()
	{
		if(fullAuto)
		{
			Shoot ();
		}
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

		var result = 0.0f;

		if(run > 0.0f)
		{
			result = Mathf.Lerp(standRecoil, runRecoil, run);
		}
		else if(crouch > 0.0f)
		{
			result =  Mathf.Lerp(standRecoil, crouchRecoil, crouch);
		}
		else
		{
			result = standRecoil;
		}

		var components = GetComponents<GS_Accuracy>();
		foreach(var component in components)
		{
			result *= component.recoilScale;
		}

		return result;
	}

	PlayerController GetPlayerController()
	{
		var player = GameObject.FindWithTag("Player");
		return player.GetComponent<PlayerController>();
	}
	
	public virtual void Reload()
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
		var bulletDirection = targetPosition - muzzle.transform.position;
		bulletDirection.Normalize();
		
		var bullet = SpawnBullet(bulletDirection).GetComponent<Bullet>();
		if(bullet)
		{
			bullet.gunIndex = (GetGun(0) == this) ? 0 : 1;
		}
		
		--magazineRemaining;
		
		PlayFireSound();
		
		ActivateMuzzleFlash();
	}

	void OnPause()
	{
		isPause = true;
	}
	
	void OnUnpause()
	{
		isPause = false;
	}
}
