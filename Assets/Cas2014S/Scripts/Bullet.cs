using UnityEngine;
using System.Collections;

public class Bullet : MyBehaviour {

	[HideInInspector]
	public float bulletDamage = 3.0f;

	[HideInInspector]
	public float explosionDamage;

    public float lifeSpan = 2.0f;

	public bool raycastBullet;

	public Vector3 direction;

	[HideInInspector]
	public GameObject shooter;

	[HideInInspector]
	public int gunIndex;

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
		mask &= ~(1 << LayerMask.NameToLayer("Item"));

		if(!Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, mask))
		{
			return;
		}

		if(hit.transform.root.gameObject == shooter)
		{
			Debug.Log("Self raycast shoot");
			return;
		}

		Debug.Log(string.Format("layer = {0}", hit.transform.gameObject.layer));

		if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Shield"))
		{
			//Debug.Log("Shield hit");
			return;
		}

		Damage(hit.transform.root.GetComponent<Health>(), hit.point);
	}

    void OnCollisionEnter(Collision collision)
    {
		if(collision.transform.root.gameObject == shooter)
		{
			//Debug.Log("Self collider shoot");
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

		var damage = health.OnBulletHit(
			new BulletHitInfo()
			{
				Damage = RandomizeDamage(),
				HitPosition = hitPosition,
			});

		AbsorbDamage(damage);
	}

	float RandomizeDamage()
	{
		var amplitude = bulletDamage * scatterDamage;

		var r = Random.Range(-amplitude, amplitude);

		return Mathf.Round(bulletDamage + r);
	}

	void AbsorbDamage(int damage)
	{
		if(shooter == null || shooter.tag != "Player")
		{
			return;
		}

		var absorb = 0.0f;
		var components = GetGun(gunIndex).GetComponents<GS_Absorb>();
		foreach(var component in components)
		{
			absorb += component.absorbPercentage;
		}

		PlayerHealth.Heal(Mathf.CeilToInt(absorb * damage * 0.01f));
	}

    protected virtual void Explode()
    {
		//Debug.Log ("Bullet::Explode");
        Destroy(gameObject);
    }

	void OnBeginFloorMove()
	{
		Destroy(gameObject);
	}
}
