using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorGenerator : MyBehaviour {

	static FloorGenerator instance;

	public static FloorGenerator Instance{get{return instance;}set{}}

	//List<GameObject> generatedObjects = new List<GameObject>();

	public GameObject exitPrefab;

	public GameObject shopPrefab;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate()
	{
		Player.transform.position = new Vector3(
			Random.value * 28.0f - 14.0f,
			2.0f,
			Random.value * 28.0f - 14.0f);

		Instantiate(
			exitPrefab,
			new Vector3(Random.value * 30.0f - 15.0f, 2.5f, Random.value * 30.0f - 15.0f),
		    Quaternion.identity);

		Instantiate(
			shopPrefab,
			new Vector3(Random.value * 30.0f - 15.0f, 0, Random.value * 30.0f - 15.0f),
			Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up));
	}

}
