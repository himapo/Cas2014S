using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float bulletDamage = 3.0f;

    public float lifeSpan = 2.0f;

	public bool raycastBullet;

	public Vector3 direction;

	[HideInInspector]
	public GameObject shooter;

	// Use this for initialization
	void Start () {
		if(raycastBullet)
		{
			RaycastShoot();
		}
	}
	
	// Update is called once per frame
	void Update () {
	    lifeSpan -= Time.deltaTime;

        if (lifeSpan <= 0)
        {
            Explode();
        }
	}

	void RaycastShoot()
	{
		RaycastHit hit;

		LayerMask mask = -1;

		mask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
		mask &= ~(1 << LayerMask.NameToLayer("Bullet"));

		if(!Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, mask))
		{
			return;
		}

		Damage(hit.transform.root.GetComponent<Health>(), hit.point);
	}

    void OnCollisionEnter(Collision collision)
    {
		if(collision.collider.gameObject == shooter)
		{
			return;
		}

		if(!raycastBullet)
		{
			Damage(collision.transform.root.GetComponent<Health>(), transform.position);
		}

        Explode();
    }

	void Damage(Health health, Vector3 hitPosition)
	{
		if(health == null)
		{
			return;
		}

		health.OnBulletHit(
			new BulletHitInfo()
			{
				Damage = bulletDamage,
				HitPosition = hitPosition,
			});
	}

    void Explode()
    {
        Destroy(gameObject);
    }
}
