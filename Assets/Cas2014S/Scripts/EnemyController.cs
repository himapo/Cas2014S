using UnityEngine;
using System.Collections;

public class EnemyController : MyBehaviour {

	public float movementSpeed;

	public float rotationRatio = 0.5f;

	public bool chasePlayer;

	public float closeDistance;

	public bool runAway;

	public bool randomWalk;

	bool playerFound;

	EnemyGun gun;

	CharacterController characterController;

	float verticalVelocity;

	// Use this for initialization
	void Start () {
		gun = GetComponent<EnemyGun>();
		characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FindPlayer();

		if(playerFound)
		{
			LookAtPlayer();
			ChasePlayer();
		}
		else
		{
			RandomWalk();
		}
	}

	void FindPlayer()
	{
		RaycastHit hit;

		var origin = new Vector3(
			transform.position.x,
			transform.position.y + 1.0f,
			transform.position.z);

		var target = new Vector3(
			Player.transform.position.x,
			Player.transform.position.y + 1.0f,
			Player.transform.position.z);

		var ray = new Ray(origin, target - origin);

		if(Physics.Raycast(ray, out hit))
		{
			playerFound = (hit.collider.tag == "Player");
		}
		else
		{
			playerFound = false;
		}
	}

	void LookAtPlayer()
	{
		var playercc = Player.GetComponent<CharacterController>();
		
		var playerTarget = Player.transform.position;
		playerTarget.y += playercc.height * 0.5f;
		
		var direction = playerTarget - gameObject.transform.position;
		var lookPlayer = Quaternion.LookRotation(direction);
		
		gameObject.transform.rotation = Quaternion.Slerp(
			gameObject.transform.rotation,
			lookPlayer,
			rotationRatio);
	}

	void ChasePlayer()
	{
		var playerTarget = Player.transform.position;
		
		var direction = playerTarget - gameObject.transform.position;

		var speed = direction.normalized * movementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

//		if(characterController.isGrounded)
//		{
//			verticalVelocity = 0;
//		}

		speed.y = verticalVelocity;

		characterController.Move(speed * Time.deltaTime);
	}

	void RandomWalk()
	{
		var speed = Vector3.zero;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		speed.y = verticalVelocity;
		
		characterController.Move(speed * Time.deltaTime);
	}

	void OnDie()
	{
		Destroy(gameObject);
	}

	void OnChangeFloor()
	{
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

	void OnShopOpen()
	{
		enabled = false;
	}
	
	void OnShopClose()
	{
		enabled = true;
	}
}
