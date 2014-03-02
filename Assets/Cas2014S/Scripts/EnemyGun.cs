using UnityEngine;
using System.Collections;

public class EnemyGun : GunBase {

	public float interval = 2.0f;

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
			var lastTime = Time.time;

			while(Time.time - lastTime < interval)
			{
				yield return null;
			}

			Fire();
		}
	}

	void Fire()
	{
		Debug.Log("Enemy shoot");

		var bulletDirection = gameObject.transform.forward;

		SpawnBullet(bulletDirection);

		PlayFireSound();
	}

}
