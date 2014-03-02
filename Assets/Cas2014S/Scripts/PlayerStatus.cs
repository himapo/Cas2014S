using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public Health health;

	// Use this for initialization
	void Start () {
		if(health == null)
		{
			var player = GameObject.FindWithTag("Player");
			health = player.GetComponent<Health>();
		}
	}
	
	// Update is called once per frame
	void Update () {

		var healthText = GetComponent<GUIText>();

		healthText.text = string.Format(
			"{0}/{1}",
			health.health,
			health.maxHealth);
	}
}
