using UnityEngine;
using System.Collections;

public class PauseMenu : MyBehaviour {

	bool isShow;

	// Use this for initialization
	void Start () {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Menu"))
		{
			ToggleShow();
		}
	}

	public void ToggleShow()
	{
		if(isShow)
		{
			Close ();
		}
		else
		{
			Open ();
		}
	}

	void Open()
	{
		isShow = true;

		Player.SendMessage("OnPauseMenuOpen", SendMessageOptions.DontRequireReceiver);
	}

	void Close()
	{
		isShow = false;

		Player.SendMessage("OnPauseMenuClose", SendMessageOptions.DontRequireReceiver);
	}

	void OnGUI()
	{
		if(!isShow)
		{
			return;
		}

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));

		GUILayout.FlexibleSpace();

		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0.3f, 0.2f, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(0.4f, 0.6f));
		
		var windowRect = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		//var windowRect = new Rect(0, 0, Screen.width, Screen.height);

        GUILayout.Window(0, windowRect, WindowFunc, "Pause Menu");

		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}

	void WindowFunc(int windowID)
	{
		var buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
		buttonStyle.fontSize = 20;

		var buttonMaxWidth = 200;
		var buttonMinHeight = 70;

		GUILayout.FlexibleSpace();

		GUILayout.BeginHorizontal();

		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button(
			"1階からやり直す",
		    buttonStyle,
			GUILayout.MaxWidth(buttonMaxWidth),
			GUILayout.MinHeight(buttonMinHeight)))
		{
			Close ();
			GameController.SendMessage("Restart");
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
			Close ();
			GameController.SendMessage("GotoTitle");
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();

		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		if(GUILayout.Button(
			"ゲームを終了する",
			buttonStyle,
			GUILayout.MaxWidth(buttonMaxWidth),
			GUILayout.MinHeight(buttonMinHeight)))
		{
			Application.Quit();
		}

		GUILayout.FlexibleSpace();

		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
	}

	void OnRestart()
	{
		enabled = true;
	}

	void OnGotoTitle()
	{
		enabled = false;
	}

	void OnGameClear()
	{
		enabled = false;
	}
}
