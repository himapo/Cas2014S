using UnityEngine;
using System.Collections;
using System;

public enum GameState
{
	Init,
	Title,
	GameMain,
	GameOver,
	Result,
}

public class GameController : MonoBehaviour {

	public GameObject player;

	public GUIText floorText;

	public GameState gameState;

	Action updateFunc;

	int floor;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		gameState = GameState.Init;
		updateFunc = StateInit;
		floor = 1;
	}
	
	// Update is called once per frame
	void Update () {
		updateFunc();
	}

	void StateInit()
	{
		updateFunc = StateTitle;
		gameState = GameState.Title;
	}

	void StateTitle()
	{
	}

	void StateGameMain()
	{
	}

	void StateGameOver()
	{
	}

	void StateResult()
	{
	}

	public void GotoNextFloor()
	{
		StartCoroutine(AsyncGotoNextFloor());
	}

	IEnumerator AsyncGotoNextFloor()
	{
		player.GetComponent<PlayerController>().enabled = false;
		player.GetComponent<GunController>().enabled = false;

		floor++;

		floorText.text = string.Format("{0}階", floor);

		var startTime = Time.time;

		while(Time.time - startTime < 3.0f)
		{
			yield return null;
		}

		player.GetComponent<PlayerController>().enabled = true;
		player.GetComponent<GunController>().enabled = true;

		floorText.text = "";
		
		Debug.Log ("Start next floor");
	}
}
