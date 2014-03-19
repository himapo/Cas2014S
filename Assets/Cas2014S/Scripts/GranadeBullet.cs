using UnityEngine;
using System.Collections;

public class GranadeBullet : Bullet {

	public GameObject explosionPrefab;

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
		var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
		var explosionComponent = explosion.GetComponent<GranadeExplosion>();

		if(explosionComponent)
		{
			explosionComponent.shooter = shooter;
			explosionComponent.gunIndex = gunIndex;
		}

		Destroy(gameObject);
	}
}
