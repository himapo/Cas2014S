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
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
