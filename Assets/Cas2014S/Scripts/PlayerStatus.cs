using UnityEngine;
using System.Collections;

public class PlayerStatus : MyBehaviour {

	static PlayerStatus instance;
	public static PlayerStatus Instance{get{return instance;}set{}}

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		var healthText = GetComponent<GUIText>();

		healthText.text = string.Format(
			"{0}/{1}",
			PlayerHealth.health,
			PlayerHealth.maxHealth);
	}
}
