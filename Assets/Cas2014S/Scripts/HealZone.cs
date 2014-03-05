using UnityEngine;
using System.Collections;

public class HealZone : MyBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDie()
	{
		GetComponentInChildren<HealCollision>().SendMessage("OnDie");

		Debug.Log("HealZone::OnDie");
		Destroy(gameObject);
	}
}
