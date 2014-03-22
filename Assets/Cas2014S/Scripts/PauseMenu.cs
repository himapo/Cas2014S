using UnityEngine;
using System.Collections;

public class PauseMenu : MyBehaviour {

	static PauseMenu instance;
	public static PauseMenu Instance{get{return instance;}set{}}

    public float mouseSensitivity;

	public bool swapTrigger;

	bool isShow;

	void Awake()
	{
		instance = this;
	}

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

		PauseCounter.Instance.Increment();
	}

	void Close()
	{
		isShow = false;

		PauseCounter.Instance.Decrement();
	}

	void OnGUI()
	{
		if(!isShow)
		{
			return;
		}

		GUILayout.BeginArea(new Rect(0,0,Screen.width, Screen.height));

		GUILayout.FlexibleSpace();

		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0.3f, 0.1f, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(0.4f, 0.7f));
		
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
		var buttonMinHeight = 60;

		GUILayout.FlexibleSpace();

		GUISoundVolume();

		GUILayout.FlexibleSpace();

		GUISensitivity();
		
		GUILayout.FlexibleSpace();

		GUISwapTrigger();
		
		GUILayout.FlexibleSpace();

		GUIRestart(buttonStyle, buttonMaxWidth, buttonMinHeight);

		GUILayout.FlexibleSpace();

		GUITitle(buttonStyle, buttonMaxWidth, buttonMinHeight);
		
		GUILayout.FlexibleSpace();

		GUIExit(buttonStyle, buttonMaxWidth, buttonMinHeight);
		
		GUILayout.FlexibleSpace();
	}

	void GUISoundVolume()
	{
		GUILayout.BeginHorizontal();

		GUILayout.FlexibleSpace();
		
		GUILayout.BeginVertical();

		GUILayout.FlexibleSpace();
		
		var labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		labelStyle.fontSize = 20;

		GUILayout.Label(
			"サウンド音量",
			labelStyle,
			GUILayout.MinWidth(140));

		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();

		//GUILayout.Space(50);
		
		GUILayout.BeginVertical();

		GUILayout.FlexibleSpace();

		AudioListener.volume = 
			GUILayout.HorizontalSlider(
				AudioListener.volume,
				0.0f,
				1.0f,
				GUILayout.MinWidth(240));

		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();

		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
	}

	void GUISensitivity()
	{
		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();
		
		var labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		labelStyle.fontSize = 20;
		
		GUILayout.Label(
			"マウス感度",
			labelStyle,
			GUILayout.MinWidth(140));
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		//GUILayout.Space(50);
		
		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();
		
		mouseSensitivity = 
			GUILayout.HorizontalSlider(
				mouseSensitivity,
				0.1f,
				20.0f,
				GUILayout.MinWidth(240));
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
	}

	void GUISwapTrigger()
	{
		GUILayout.BeginHorizontal();
		
		//GUILayout.FlexibleSpace();

		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();

		var toggleStyle = new GUIStyle(GUI.skin.GetStyle("toggle"));
		toggleStyle.fontSize = 20;
		toggleStyle.alignment = TextAnchor.MiddleCenter;

		swapTrigger = 
			GUILayout.Toggle(
				swapTrigger,
				"トリガー左右入れ替え",
				toggleStyle);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		//GUILayout.FlexibleSpace();
		
		GUILayout.EndHorizontal();
	}

	void GUIRestart(GUIStyle buttonStyle, float buttonMaxWidth, float buttonMinHeight)
	{
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
	}

	void GUITitle(GUIStyle buttonStyle, float buttonMaxWidth, float buttonMinHeight)
	{
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
	}

	void GUIExit(GUIStyle buttonStyle, float buttonMaxWidth, float buttonMinHeight)
	{
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

	void OnGameOver()
	{
		enabled = false;
	}
}
