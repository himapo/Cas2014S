using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;

    public float mouseSensitivity;

    public float jumpSpeed;

    public float upDownRange;

    float verticalRotation;

    float verticalVelocity;

	public float crouchSpeed;

	[HideInInspector]
	public float crouch { get; set; }	// 0.0 - 1.0

	[HideInInspector]
	public float run { get; set; }	// 0.0 - 1.0

	// Use this for initialization
	void Start () {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Screen.lockCursor = true;

        var characterController = GetComponent<CharacterController>();

        var rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        var forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        var sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

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
            verticalVelocity = jumpSpeed;
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

        characterController.Move(speed * Time.deltaTime);

		var horizontalSpeed = new Vector2(sideSpeed, forwardSpeed);
		run = horizontalSpeed.magnitude / movementSpeed;
	}

	void OnShopOpen()
	{
		enabled = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	void OnShopClose()
	{
		enabled = true;
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}

	void OnPauseMenuOpen()
	{
		enabled = false;
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}

	void OnPauseMenuClose()
	{
		enabled = true;
		Screen.lockCursor = true;
		Screen.showCursor = false;
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
}
