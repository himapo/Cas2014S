using UnityEngine;
using System.Collections;

public class WarriorController : MyBehaviour {

	public float rotationRatio = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var characterController = Player.GetComponent<CharacterController>();

		var playerTarget = Player.transform.position;
		playerTarget.y += characterController.height * 0.5f;

		var direction = playerTarget - gameObject.transform.position;
		var lookPlayer = Quaternion.LookRotation(direction);

		gameObject.transform.rotation = Quaternion.Slerp(
			gameObject.transform.rotation,
			lookPlayer,
			rotationRatio);
	}

	void OnDie()
	{
		Destroy(gameObject);
	}

	void OnChangeFloor()
	{
		Destroy(gameObject);
	}

	void OnShopOpen()
	{
		enabled = false;
	}
	
	void OnShopClose()
	{
		enabled = true;
	}
}
