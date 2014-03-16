using UnityEngine;
using System.Collections;

public class Bullet : MyBehaviour {

	[HideInInspector]
	public float bulletDamage = 3.0f;

    public float lifeSpan = 2.0f;

	public bool raycastBullet;

	public Vector3 direction;

	[HideInInspector]
	public GameObject shooter;

	public float scatterDamage = 0.2f;

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
		mask &= ~(1 << LayerMask.NameToLayer("Zone"));

		if(!Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, mask))
		{
			return;
		}

		if(hit.transform.root.gameObject == shooter)
		{
			Debug.Log("Self shoot");
			return;
		}

		Damage(hit.transform.root.GetComponent<Health>(), hit.point);
	}

    void OnCollisionEnter(Collision collision)
    {
		if(collision.transform.root.gameObject == shooter)
		{
			Debug.Log("Self shoot");
			return;
		}

		if(!raycastBullet)
		{
			Damage(collision.transform.root.GetComponent<Health>(), transform.position);
		}

        Explode();
    }

	protected void Damage(Health health, Vector3 hitPosition)
	{
		if(health == null)
		{
			return;
		}

		health.OnBulletHit(
			new BulletHitInfo()
			{
				Damage = RandomizeDamage(),
				HitPosition = hitPosition,
			});
	}

	float RandomizeDamage()
	{
		var amplitude = bulletDamage * scatterDamage;

		var r = Random.Range(-amplitude, amplitude);

		return Mathf.Round(bulletDamage + r);
	}

    protected virtual void Explode()
    {
		Debug.Log ("Bullet::Explode");
        Destroy(gameObject);
    }

	void OnBeginFloorMove()
	{
		Destroy(gameObject);
	}
}
