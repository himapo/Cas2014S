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

		var bulletDirection = GetPlayerTargetPosition() - muzzle.transform.position;
		bulletDirection.Normalize();

		SpawnBullet(bulletDirection);

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
