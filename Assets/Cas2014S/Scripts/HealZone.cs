using UnityEngine;
using System.Collections;

public class HealZone : MyBehaviour {

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

		PlayerHealth.healingSpeed += 1.0f;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag != "Player")
		{
			return;
		}

		PlayerHealth.healingSpeed -= 1.0f;
	}
}
