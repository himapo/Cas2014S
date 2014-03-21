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
			var up = Mathf.RoundToInt(Random.Range(0.6f, 1.4f) * maxHealthUp);

			PlayerHealth.AddMaxHealth(up);

			var go = Instantiate(healTextPrefab) as GameObject;
			var healText = go.GetComponent<HealText>();
			healText.heal = Mathf.RoundToInt(up);
		}

		var audio = GetComponent<AudioSource>();
		
		if(audio != null)
		{
			audio.Play();
			enabled = false;
			StartCoroutine(AsyncDestroy());
		}
		else
		{
			Destroy(gameObject);
		}
	}

	IEnumerator AsyncDestroy()
	{
		var audio = GetComponent<AudioSource>();

		if(audio == null)
		{
			yield break;
		}

		while(audio.isPlaying)
		{
			yield return null;
		}

		Destroy(gameObject);
	}

	void OnBeginFloorMove()
	{
		Destroy(gameObject);
	}
	
	void OnGotoTitle()
	{
		Destroy(gameObject);
	}
}
