using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GranadeExplosion : Bullet {

	public GameObject detonatorPrefab;

	public float radius = 4.0f;

	bool damageFinished = false;

	// Use this for initialization
	void Start () {
		Instantiate(detonatorPrefab, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}

	void LateUpdate()
	{
		//return;

		if(damageFinished)
		{
//			var collider = GetCollider();
//			if(collider != null)
//			{
//				Destroy(collider);
//			}
		}
		else
		{
			//sphereCollider.enabled = true;
			damageFinished = true;

			var colliders = Physics.OverlapSphere(transform.position, radius);

			var healthes = new List<Health>();

			foreach(var collider in colliders)
			{
				if(collider.gameObject.layer == LayerMask.NameToLayer("Zone"))
				{
					continue;
				}

				var health = collider.transform.root.GetComponent<Health>();

				if(health != null && !healthes.Contains(health))
				{
					healthes.Add(health);
					Damage(health, collider.transform.position);
				}
			}
		}
	}

	SphereCollider GetCollider()
	{
		return GetComponent<SphereCollider>();
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Explosion CollisionEnter");
	}

	void OnTriggerStay(Collider collider)
	{
		//Debug.Log ("Explosion TriggerStay");

//		if(collider.transform.root.gameObject == shooter)
//		{
//			Debug.Log("Self shoot");
//			return;
//		}

		//Damage(collider.transform.root.GetComponent<Health>(), transform.position);

		//damageFinished = true;
	}

	protected override void Explode ()
	{
		Debug.Log ("GranadeExplosion::Explode");
		Destroy(gameObject);
	}
}
