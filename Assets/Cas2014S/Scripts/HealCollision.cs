using UnityEngine;
using System.Collections;

public class HealCollision: MyBehaviour {

	public float healingSpeed = 1.0f;

	bool playerEnter = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Player")
		{
			return;
		}

		PlayerHealth.healingSpeed += healingSpeed;

		playerEnter = true;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag != "Player")
		{
			return;
		}

		PlayerHealth.healingSpeed -= healingSpeed;

		playerEnter = false;
	}

	void OnDie()
	{
		Debug.Log("HealCollision::OnDie");
		if(playerEnter)
		{
			PlayerHealth.healingSpeed -= healingSpeed;
			playerEnter = false;
		}
	}
}
