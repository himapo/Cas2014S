using UnityEngine;
using System.Collections;

public class EnemyGun : GunBase {

	public float interval = 2.0f;

	public float startDistance;

	bool firing;

	bool stopFire = false;

	float lastTime;

	float timeRemaining;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartShoot()
	{
		if(firing)
		{
			return;
		}

		firing = true;

		StartCoroutine(AsyncFire());
	}

	public void StopShoot()
	{
		firing = false;
	}

	IEnumerator AsyncFire()
	{
		// 最初の発砲は早くする
		lastTime = Time.time - Random.value * interval * 0.2f;
		
		while(firing)
		{
			while(stopFire || Time.time - lastTime < interval)
			{
				yield return null;
			}

			Fire();

			lastTime = Time.time;
		}
	}

	void Fire()
	{
		//Debug.Log("Enemy shoot");

		var bulletDirection = GetPlayerTargetPosition() - muzzle.transform.position;

		if(bulletDirection.magnitude > startDistance)
		{
			return;
		}

		bulletDirection.Normalize();

		var bullet = SpawnBullet(bulletDirection);
		var bulletComponent = bullet.GetComponent<Bullet>();
		bulletComponent.bulletDamage = bulletDamage;
		bulletComponent.explosionDamage = explosionDamage;

		PlayFireSound();
	}

	Vector3 GetPlayerTargetPosition()
	{
		var playercc = Player.GetComponent<CharacterController>();
		
		var playerTarget = new Vector3(
			Player.transform.position.x,
			Player.transform.position.y,
			Player.transform.position.z);
		playerTarget.y += playercc.height * 0.5f;
		
		return playerTarget;
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

	void OnGameClear()
	{
		StopFire();
	}

	void OnGameOver()
	{
		StopFire();
	}

	void OnPause()
	{
		StopFire();
	}
	
	void OnUnpause()
	{
		StartFire ();
	}
}
