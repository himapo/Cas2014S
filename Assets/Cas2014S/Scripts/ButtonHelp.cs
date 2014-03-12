using UnityEngine;
using System.Collections;

public class ButtonHelp : MonoBehaviour {
	
	public Vector3 helpSize;

	public Vector3 shopPosition;

	public Vector3 leftReloadPosition;

	public Vector3 rightReloadPosition;

	GUIStyle buttonStyle;

	GUIStyle labelStyle;

	bool isShowShop = false;

	Rect shopArea;

	// Use this for initialization
	void Start () {
		var topleft = Camera.main.ViewportToScreenPoint(shopPosition);
		
		var size = Camera.main.ViewportToScreenPoint(helpSize);
		
		shopArea = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowShop()
	{
		isShowShop = true;
	}

	public void HideShop()
	{
		isShowShop = false;
	}

	void OnGUI()
	{
		if(buttonStyle == null)
		{
			buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
			buttonStyle.fontSize = 20;
		}
		
		if(labelStyle == null)
		{
			labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
			labelStyle.fontSize = 30;
		}

		if(isShowShop)
		{
			ShowGUI("E", "ショップ");
		}
	}

	void ShowGUI(string button, string label)
	{
		GUILayout.BeginArea(shopArea);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.Button(button, buttonStyle, GUILayout.MaxWidth(50), GUILayout.MinHeight(50));
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.Label(label, labelStyle);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}
}
