using UnityEngine;
using System.Collections;
using System;

public class GameController : MyBehaviour {

	public GUIText floorText;

	Action updateFunc;

	Action guiFunc;

	int floor;
	
	// Use this for initialization
	void Start () {
		updateFunc = StateInit;
		guiFunc = ()=>{};
		floor = 1;

		BroadcastAll("OnApplicationStart");
	}
	
	// Update is called once per frame
	void Update () {
		updateFunc();
	}

	void OnGUI()
	{
		guiFunc();
	}

	void StateInit()
	{
		updateFunc = StateTitle;
		guiFunc = GUITitle;
	}

	void StateTitle()
	{
	}

	void GUITitle()
	{
		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(1.0f, 0.5f, 0.0f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		GUILayout.BeginArea(area);

		GUILayout.FlexibleSpace();

		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 200;

		GUILayout.Label("伽洲怪島", guiStyle);

		GUILayout.FlexibleSpace();

		GUILayout.EndArea();

		topleft = Camera.main.ViewportToScreenPoint(new Vector3(0.4f, 0.5f, 0));
		
		size = Camera.main.ViewportToScreenPoint(new Vector3(0.2f, 0.5f, 0.0f));
		
		area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		GUILayout.BeginArea(area);
		
		GUILayout.FlexibleSpace();

		GUILayout.BeginHorizontal();

		GUILayout.FlexibleSpace();

		var buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
		buttonStyle.fontSize = 20;

		if(GUILayout.Button(
			"島へ行く",
			buttonStyle,
			GUILayout.MaxWidth(200),
			GUILayout.MinHeight(50)))
		{
			Restart();
		}

		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}

	void StateBeginFloorMove()
	{
		BroadcastAll("OnBeginFloorMove");

		updateFunc = StateFloorMove;
		guiFunc = GUIFloorMove;

		StartCoroutine(AsyncFloorMove());
	}

	void StateFloorMove()
	{
	}

	void GUIFloorMove()
	{
		// TODO : Fade

		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(1.0f, 1.0f, 0.0f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
		
		GUILayout.BeginArea(area);
		
		GUILayout.FlexibleSpace();
		
		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 200;
		
		GUILayout.Label(string.Format("{0}階", floor), guiStyle);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}

	IEnumerator AsyncFloorMove()
	{
		var startTime = Time.time;
		
		while(Time.time - startTime < 3.0f)
		{
			yield return null;
		}
		
		Debug.Log ("Start next floor");

		BroadcastAll("OnEndFloorMove");
	
		updateFunc = StateGameMain;
		guiFunc = ()=>{};
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

	public void Restart()
	{
		BroadcastAll("OnRestart");

		floor = 0;

		GotoNextFloor();
	}

	public void GotoNextFloor()
	{
		floor++;
		updateFunc = StateBeginFloorMove;
		guiFunc = ()=>{};

		BroadcastAll("OnGotoNextFloor");
	}

	public void GotoTitle()
	{
		updateFunc = StateTitle;
		guiFunc = ()=>{};
		
		BroadcastAll("OnGotoTitle");
	}
}
