using UnityEngine;
using System.Collections;
using System;

public class GameController : MyBehaviour {

	public GUIText floorText;

	public int maxFloor;

	public GameObject playerPrefab;

	Action updateFunc;

	Action guiFunc;

	int floor;

	bool abortMoveFloor;

	float gameStartTime;

	float gameEndTime;

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
		GotoTitle();
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

		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button(
			"ゲームを終了する",
			buttonStyle,
			GUILayout.MaxWidth(200),
			GUILayout.MinHeight(50)))
		{
			Application.Quit();
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		GUILayout.EndArea();
	}

	void StateBeginFloorMove()
	{
		BroadcastAll("OnBeginFloorMove");

		FloorGenerator.Instance.Generate(floor);

		updateFunc = StateFloorMove;
		guiFunc = GUIFloorMove;

		abortMoveFloor = false;

		StartCoroutine(AsyncFloorMove());
	}

	void StateFloorMove()
	{
	}

	void GUIFloorMove()
	{
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
		
		while(!abortMoveFloor && Time.time - startTime < 0.5f)
		{
			yield return null;
		}

		if(abortMoveFloor)
		{
			yield break;
		}
		
		Debug.Log ("Start next floor");

		BroadcastAll("OnEndFloorMove");
	
		Fader.Instance.FadeIn(0.5f);
		
		updateFunc = StateGameMain;
		guiFunc = ()=>{};
	}

	void StateGameMain()
	{
		if(PlayerHealth.health <= 0)
		{
			gameEndTime = Time.time;
			updateFunc = StateResult;
			guiFunc = GUIGameOverResult;
			BroadcastAll("OnGameOver");
		}
	}

	void StateGameOver()
	{
	}

	void StateResult()
	{

	}

	void GUIClearResult()
	{
		GUIResult(ClearWindowFunc, "Clear Result");
	}

	void GUIGameOverResult()
	{
		GUIResult(GameOverWindowFunc, "Game Over");
	}

	void GUIResult(GUI.WindowFunction windowFunc, string label)
	{
		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));
		
		GUILayout.FlexibleSpace();
		
		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0.2f, 0.1f, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(0.6f, 0.8f));
		
		var windowRect = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		GUILayout.Window(0, windowRect, windowFunc, label);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}

	void ClearWindowFunc(int windowID)
	{
		var labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		labelStyle.fontSize = 50;

		var buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
		buttonStyle.fontSize = 20;
		
		var buttonMaxWidth = 200;
		var buttonMinHeight = 70;

		//GUILayout.FlexibleSpace();

		GUILayout.Label(
			"伽洲怪島クリア！",
			labelStyle);

		//GUILayout.FlexibleSpace();

		var finishTime = gameEndTime - gameStartTime;
		var minutes = Mathf.FloorToInt(finishTime / 60.0f);
		var seconds = Mathf.FloorToInt(finishTime - 60.0f * minutes);
		
		GUILayout.Label(
			string.Format("{0}分{1}秒", minutes, seconds),
			labelStyle);

		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button(
			"1階からやり直す",
			buttonStyle,
			GUILayout.MaxWidth(buttonMaxWidth),
			GUILayout.MinHeight(buttonMinHeight)))
		{
			Restart();
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();

		if(GUILayout.Button(
			"タイトルに戻る",
			buttonStyle,
			GUILayout.MaxWidth(buttonMaxWidth),
			GUILayout.MinHeight(buttonMinHeight)))
		{
			GotoTitle();
		}

		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
	}

	void GameOverWindowFunc(int windowID)
	{
		var labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		labelStyle.fontSize = 50;
		
		var buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
		buttonStyle.fontSize = 20;
		
		var buttonMaxWidth = 200;
		var buttonMinHeight = 70;
		
		//GUILayout.FlexibleSpace();
		
		GUILayout.Label(
			string.Format("{0}階で力尽きた", floor),
			labelStyle);
		
		//GUILayout.FlexibleSpace();
		
		var finishTime = gameEndTime - gameStartTime;
		var minutes = Mathf.FloorToInt(finishTime / 60.0f);
		var seconds = Mathf.FloorToInt(finishTime - 60.0f * minutes);
		
		GUILayout.Label(
			string.Format("{0}分{1}秒", minutes, seconds),
			labelStyle);

		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button(
			"1階からやり直す",
			buttonStyle,
			GUILayout.MaxWidth(buttonMaxWidth),
			GUILayout.MinHeight(buttonMinHeight)))
		{
			Restart();
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button(
			"タイトルに戻る",
			buttonStyle,
			GUILayout.MaxWidth(buttonMaxWidth),
			GUILayout.MinHeight(buttonMinHeight)))
		{
			GotoTitle();
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
	}

	public void Restart()
	{
		gameStartTime = Time.time;

		Destroy(Player);

		var player = Instantiate(playerPrefab) as GameObject;

		ResetPlayer(player);

		BroadcastAll("OnRestart");

		abortMoveFloor = true;
		floor = 0;

		GotoNextFloor();
	}

	public void GotoNextFloor()
	{
		floor++;

		if(floor < maxFloor)
		{
			Fader.Instance.FadeOut(0.0001f);
			updateFunc = StateBeginFloorMove;
			guiFunc = ()=>{};
			BroadcastAll("OnGotoNextFloor");
		}
		else
		{
			gameEndTime = Time.time;
			updateFunc = StateResult;
			guiFunc = GUIClearResult;
			BroadcastAll("OnGameClear");
		}
	}

	public void GotoTitle()
	{
		updateFunc = StateTitle;
		guiFunc = GUITitle;

		abortMoveFloor = true;
		Fader.Instance.FadeOut(0.0001f);
		
		BroadcastAll("OnGotoTitle");
	}
}
