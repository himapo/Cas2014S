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

	[HideInInspector]
	public EnemyGun gun;

	CharacterController characterController;

	float verticalVelocity;

	Vector3 randomDirection;

	// Use this for initialization
	void Start () {
		gun = GetComponent<EnemyGun>();
		characterController = GetComponent<CharacterController>();

		var rand = Random.value * 360.0f;
		randomDirection = new Vector3(
			Mathf.Cos(rand),
			0.0f,
			Mathf.Sin(rand));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		FindPlayer();

		if(chasePlayer)
		{
			if(playerFound)
			{
				LookAtPlayer();
				ChasePlayer();
				StartShoot();
			}
			else
			{
				if(randomWalk)
				{
					RandomWalk();
				}
				StopShoot();
			}
		}
		else if(runAway)
		{
			if(playerFound)
			{
				RunAway();
			}
			else if(randomWalk)
			{
				RandomWalk();
			}
		}
		else if(randomWalk)
		{
			if(playerFound)
			{
				LookAtPlayer();
				StartShoot();
			}
			else
			{
				StopShoot();
			}
			RandomWalk();
		}
		else
		{
			if(playerFound)
			{
				LookAtPlayer();
				StartShoot();
			}
			else
			{
				StopShoot();
			}
		}
	}

	void FindPlayer()
	{
		RaycastHit hit;

		var origin = new Vector3(
			transform.position.x,
			transform.position.y + 1.0f,
			transform.position.z);

		var target = GetPlayerTargetPosition();

		var ray = new Ray(origin, target - origin);

		LayerMask mask = -1;
		
		mask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
		mask &= ~(1 << LayerMask.NameToLayer("Enemy"));
		mask &= ~(1 << LayerMask.NameToLayer("Item"));
		mask &= ~(1 << LayerMask.NameToLayer("Shop"));
		mask &= ~(1 << LayerMask.NameToLayer("Shield"));
		mask &= ~(1 << LayerMask.NameToLayer("Bullet"));
		
		if(Physics.Raycast(ray, out hit, float.MaxValue, mask))
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
		var playerTarget = GetPlayerTargetPosition();

		//Debug.Log(string.Format("PlayerTarget.y = {0}", playerTarget.y));
		
		var direction = playerTarget - gameObject.transform.position;
		var lookPlayer = Quaternion.LookRotation(direction);
		
		gameObject.transform.rotation = Quaternion.Slerp(
			gameObject.transform.rotation,
			lookPlayer,
			rotationRatio);
	}

	Vector3 GetPlayerTargetPosition()
	{
		var playercc = Player.GetComponent<CharacterController>();
		
		var playerTarget = new Vector3(
			Player.transform.position.x,
			Player.transform.position.y,
			Player.transform.position.z);
		playerTarget.y += playercc.height * 0.5f;

		return playerTarget;
	}

	void ChasePlayer()
	{
		var playerTarget = Player.transform.position;
		
		var direction = playerTarget - gameObject.transform.position;

		var speed = direction.normalized * movementSpeed;

		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		speed.y = verticalVelocity;

		if(direction.magnitude < closeDistance)
		{
			speed.x = speed.z = 0;
		}

		characterController.Move(speed * Time.deltaTime);
	}

	void RandomWalk()
	{
		var speed = randomDirection * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		speed.y = verticalVelocity;

		var lookRotation = Quaternion.LookRotation(randomDirection);

		gameObject.transform.rotation = Quaternion.Slerp(
			gameObject.transform.rotation,
			lookRotation,
			rotationRatio);
		
		characterController.Move(speed * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//Debug.Log ("hit");

		var layer = hit.collider.transform.gameObject.layer;

		if(randomWalk
		   && (layer == LayerMask.NameToLayer("Wall") || layer == LayerMask.NameToLayer("OutWall")))
		{
			var normal = hit.normal;
			
			var rand = Random.Range(-45.0f, 45.0f);
			
			var rot = Quaternion.AngleAxis(rand, Vector3.up);
			
			randomDirection = rot * normal;
		}
	}

	void RunAway()
	{
		var playerTarget = Player.transform.position;
		
		var direction = gameObject.transform.position - playerTarget;
		
		var speed = direction.normalized * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		speed.y = verticalVelocity;

		var lookRotation = Quaternion.LookRotation(direction);
		
		gameObject.transform.rotation = Quaternion.Slerp(
			gameObject.transform.rotation,
			lookRotation,
			rotationRatio);
		
		characterController.Move(speed * Time.deltaTime);
	}

	void StartShoot()
	{
		gun.StartShoot();
	}

	void StopShoot()
	{
		gun.StopShoot();
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

	void OnPauseMenuOpen()
	{
		enabled = false;
	}
	
	void OnPauseMenuClose()
	{
		enabled = true;
	}

	void OnGameOver()
	{
		enabled = false;
	}

	void OnGameClear()
	{
		enabled = false;
	}
}
