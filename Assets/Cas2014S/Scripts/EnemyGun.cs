using UnityEngine;
using System.Collections;

public class EnemyGun : GunBase {

	public float interval = 2.0f;

	bool stopFire = false;

	float lastTime;

	float timeRemaining;

	// Use this for initialization
	void Start () {
		StartCoroutine(AsyncFire());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator AsyncFire()
	{
		while(true)
		{
			lastTime = Time.time;

			while(stopFire || Time.time - lastTime < interval)
			{
				yield return null;
			}

			Fire();
		}
	}

	void Fire()
	{
		//Debug.Log("Enemy shoot");

		var bulletDirection = gameObject.transform.forward;

		SpawnBullet(bulletDirection);

		PlayFireSound();
	}

	void StopFire()
	{
		stopFire = true;
		timeRemaining = Time.time - lastTime;
	}

	void StartFire()
	{
		stopFire = false;
		lastTime = Time.time - timeRemaining;
	}

	void OnShopOpen()
	{
		StopFire();
	}
	
	void OnShopClose()
	{
		StartFire();
	}

	void OnGameClear()
	{
		StopFire();
	}

	void OnGameOver()
	{
		StopFire();
	}

	void OnPauseMenuOpen()
	{
		StopFire();
	}

	void OnPauseMenuClose()
	{
		StartFire();
	}
}
