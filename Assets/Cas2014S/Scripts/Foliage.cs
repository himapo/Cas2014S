using UnityEngine;
using System.Collections;

public class Foliage : MyBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBeginFloorMove()
	{
		Destroy(gameObject);
	}
}
