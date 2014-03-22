using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MyBehaviour {

    public float movementSpeed;

    public float mouseSensitivity;

    public float jumpSpeed;

    public float upDownRange;

    float verticalRotation;

    float verticalVelocity;

	public float height;

	public float crouchDown;

	public float crouchSpeed;	

	[HideInInspector]
	public float crouch { get; set; }	// 0.0 - 1.0

	[HideInInspector]
	public float run { get; set; }	// 0.0 - 1.0

	// Use this for initialization
	void Start () {
		//enabled = false;
		Player.transform.position = GameController.startPosition;
	}
	
	// Update is called once per frame
	void Update () {

		Screen.lockCursor = true;

        var characterController = GetComponent<CharacterController>();

        var rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
        transform.Rotate(0, rotLeftRight, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.timeScale;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		var forwardSpeed = Input.GetAxis("Vertical") * GetMovementSpeed();
		var sideSpeed = Input.GetAxis("Horizontal") * GetMovementSpeed();

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

		if(characterController.isGrounded && Input.GetButton("Crouch"))
		{
			crouch += crouchSpeed * Time.deltaTime;
			crouch = Mathf.Clamp(crouch, 0.0f, 1.0f);
		}
		else if(crouch > 0.0f)
		{
			crouch -= crouchSpeed * Time.deltaTime;
			crouch = Mathf.Clamp(crouch, 0.0f, 1.0f);
		}
        else if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Jump");
            verticalVelocity = GetJumpSpeed();
        }

        var speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;

		if(crouch > 0.0f)
		{
			speed *= 0.5f;
		}

		Camera.main.transform.localPosition = new Vector3(
			0.0f,
			1.75f - crouch,
			0.0f);

//		characterController.height = height - crouch * crouchDown;
//		characterController.center = new Vector3(
//			characterController.center.x,
//			characterController.height * 0.51f,
//			characterController.center.z);

        characterController.Move(speed * Time.deltaTime);
		
		var horizontalSpeed = new Vector2(sideSpeed, forwardSpeed);
		run = horizontalSpeed.magnitude / GetMovementSpeed();

		CheckPakorepu();
	}

	float GetMovementSpeed()
	{
		var result = movementSpeed;
		var components = new List<PS_SpeedUp>();
		components.AddRange(GetGun(0).GetComponents<PS_SpeedUp>());
		components.AddRange(GetGun(1).GetComponents<PS_SpeedUp>());
		foreach(var component in components)
		{
			result += component.movementSpeed;
		}
		return result;
	}

	float GetJumpSpeed()
	{
		var result = jumpSpeed;
		var components = new List<PS_JumpUp>();
		components.AddRange(GetGun(0).GetComponents<PS_JumpUp>());
		components.AddRange(GetGun(1).GetComponents<PS_JumpUp>());
		foreach(var component in components)
		{
			result += component.jumpSpeed;
		}
		return result;
	}

	void CheckPakorepu()
	{
		var num = GetGun(0).GetComponents<PS_Pakorepu>().Length;
		num += GetGun(1).GetComponents<PS_Pakorepu>().Length;

		if(num > 0)
		{
			gameObject.layer = LayerMask.NameToLayer("Pakorepu");
		}
		else
		{
			gameObject.layer = LayerMask.NameToLayer("Default");
		}
	}

	void OnControllerCollideHit(ControllerColliderHit hit)
	{

	}

	void OnDamage()
	{
		Fader.Instance.RedFadeIn(0.4f);
	}

	void OnBeginFloorMove()
	{
		enabled = false;
	}

	void OnEndFloorMove()
	{
		enabled = true;
	}

	void OnRestart()
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

	void OnGotoTitle()
	{
		enabled = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	void OnGameClear()
	{
		enabled = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	void OnGameOver()
	{
		enabled = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	void OnPause()
	{
		enabled = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	void OnUnpause()
	{
		enabled = true;
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
}
