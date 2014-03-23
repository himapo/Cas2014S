using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ButtonHelpInfo
{
	public string helpName;

	public Vector3 position;

	public Vector3 size;

	public string buttonString;
	
	public float buttonSize;
	
	public int buttonFontSize;
	
	public string labelString;
	
	public int labelFontSize;

	public bool swap;
	
	public Rect area{get;set;}

	public bool isShow{get;set;}

	[HideInInspector]
	public GUIStyle buttonStyle;

	[HideInInspector]
	public GUIStyle labelStyle;
	
	public void CalcRect()
	{
		var topleft = Camera.main.ViewportToScreenPoint(position);
		
		var screenSize = Camera.main.ViewportToScreenPoint(size);
		
		area = new Rect(
			topleft.x, topleft.y,
			screenSize.x, screenSize.y);
	}

	public void CreateStyle()
	{
		//if(buttonStyle == null)
		{
			buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
			buttonStyle.fontSize = buttonFontSize;
		}
		
		//if(labelStyle == null)
		{
			labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
			labelStyle.fontSize = labelFontSize;
		}
	}
}

public class ButtonHelp : MyBehaviour {

	static ButtonHelp instance;
	public static ButtonHelp Instance{get{return instance;}set{}}

	public List<ButtonHelpInfo> helpInfos;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		foreach(var info in helpInfos)
		{
			info.CalcRect();
		}

		SetShow("leftReload", true);
		SetShow("rightReload", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetShow(string name, bool isShow)
	{
		helpInfos.FirstOrDefault((info)=>{
			return info.helpName == name;
		}).isShow = isShow;
	}

	void OnGUI()
	{
		GUI.depth = 3;

		foreach(var info in helpInfos)
		{
			if(!info.isShow)
			{
				continue;
			}

			info.CreateStyle();
			
			ShowGUI(
				info.area,
				info.buttonString,
				info.buttonSize,
				info.labelString,
				info.swap,
				info.buttonStyle,
				info.labelStyle);
		}
	}

	void ShowGUI(
		Rect area, string button, float buttonSize, string label, bool swap,
		GUIStyle buttonStyle, GUIStyle labelStyle)
	{		
		GUILayout.BeginArea(area);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.BeginVertical();

		GUILayout.FlexibleSpace();

		if(swap)
		{
			GUILayout.Label(label, labelStyle);
		}
		else
		{
			GUILayout.Label(button, buttonStyle, GUILayout.MaxWidth(buttonSize), GUILayout.MinHeight(buttonSize));
		}

		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();
		
		if(swap)
		{
			GUILayout.Label(button, buttonStyle, GUILayout.MaxWidth(buttonSize), GUILayout.MinHeight(buttonSize));
		}
		else
		{
			GUILayout.Label(label, labelStyle);
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}
}
