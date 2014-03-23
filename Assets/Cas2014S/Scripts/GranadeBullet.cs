using UnityEngine;
using System.Collections;

public class GranadeBullet : Bullet {

	public GameObject explosionPrefab;

	bool exploded;

	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}

	protected override void Explode ()
	{
		if(exploded)
		{
			return;
		}

		exploded = true;

		var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
		var explosionComponent = explosion.GetComponent<GranadeExplosion>();

		if(explosionComponent)
		{
			explosionComponent.shooter = shooter;
			explosionComponent.gunIndex = gunIndex;
			explosionComponent.bulletDamage = explosionDamage;
		}

		Destroy(gameObject);
	}
}
