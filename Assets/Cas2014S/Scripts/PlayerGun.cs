using UnityEngine;
using System.Collections;

public class PlayerGun : GunBase
{
	public float minStandRecoil = 1.0f;
	public float maxStandRecoil = 2.0f;
	public float minRunRecoil = 2.0f;
	public float maxRunRecoil = 4.0f;
	public float minCrouchRecoil = 0.5f;
	public float maxCrouchRecoil = 1.0f;
	
	// 次弾発射までの間隔
	public float intervalTime = 0.1f;
	
	// 0.0で次弾発射可能
	float intervalRemaining;
	
	public float reloadSpeed = 1.0f;
	
	// 1.0でリロード完了
	float reloadDuration;
	
	public int magazineSize = 10;
	
	public int magazineRemaining{ get; protected set; }
	
	// Use this for initialization
	void Start()
	{
		intervalRemaining = 0.0f;
		reloadDuration = 1.0f;
		magazineRemaining = magazineSize;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(IsReloading())
		{
			reloadDuration += reloadSpeed * Time.deltaTime;
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
		
		Fire (targetPosition);
		
		intervalRemaining = intervalTime;
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
		var bulletDirection = targetPosition - muzzle.transform.position;
		bulletDirection.Normalize();
		
		var bullet = SpawnBullet(bulletDirection);
		
		--magazineRemaining;
		
		PlayFireSound();
		
		ActivateMuzzleFlash();
	}
}
