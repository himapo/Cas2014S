using UnityEngine;
using System.Collections;

public class HealItem : MyBehaviour {

	public float healPercentage;

	public int maxHealthUp;

	public GameObject healTextPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag != "Player")
		{
			return;
		}

		if(healPercentage > 0.0f)
		{
			if(PlayerHealth.health >= PlayerHealth.maxHealth)
			{
				return;
			}

			var heal = PlayerHealth.Heal(healPercentage);

			var go = Instantiate(healTextPrefab) as GameObject;
			var healText = go.GetComponent<HealText>();
			healText.heal = heal;
		}

		if(maxHealthUp > 0)
		{
			PlayerHealth.AddMaxHealth(maxHealthUp);

			var go = Instantiate(healTextPrefab) as GameObject;
			var healText = go.GetComponent<HealText>();
			healText.heal = maxHealthUp;
		}

		Destroy(gameObject);
	}
}
