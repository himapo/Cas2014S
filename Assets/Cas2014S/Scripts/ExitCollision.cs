using UnityEngine;
using System.Collections;

public class ExitCollision : MonoBehaviour {

	public float stayTime = 3.0f;

	public GameObject gameController;

	float elapsed;

	bool enter;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindWithTag("GameController");
		elapsed = 0;
		enter = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		enter = true;
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			elapsed += Time.deltaTime;
		}

		if(enter && elapsed >= stayTime)
		{
			Debug.Log("Go to Next Floor");
			gameController.SendMessage("GotoNextFloor");
			enter = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			elapsed = 0;
			enter = false;
		}
	}
}
