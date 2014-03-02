using UnityEngine;
using System.Collections;

public class WarriorController : MonoBehaviour {

	public GameObject player;

	public float rotationRatio = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var characterController = player.GetComponent<CharacterController>();

		var playerTarget = player.transform.position;
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
}
