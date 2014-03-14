using UnityEngine;
using System.Collections;

public class PauseMenu : MyBehaviour {

	bool isShow;

	// Use this for initialization
	void Start () {
	
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

		var windowRect = new Rect(500, 100, 400, 500);

        GUILayout.Window(0, windowRect, WindowFunc, "Pause Menu");

		GUILayout.EndArea();
	}

	void WindowFunc(int windowID)
	{
		GUILayout.FlexibleSpace();

		if(GUILayout.Button("Restart"))
		{
			Close ();
			GameController.SendMessage("Restart");
		}

		if(GUILayout.Button("Exit Game"))
		{
			Application.Quit();
		}

		GUILayout.FlexibleSpace();
	}
}
