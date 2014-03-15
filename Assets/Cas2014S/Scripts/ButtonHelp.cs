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
	
	public void CalcRect()
	{
		var topleft = Camera.main.ViewportToScreenPoint(position);
		
		var screenSize = Camera.main.ViewportToScreenPoint(size);
		
		area = new Rect(
			topleft.x, topleft.y,
			screenSize.x, screenSize.y);
	}
}

public class ButtonHelp : MyBehaviour {

	public List<ButtonHelpInfo> helpInfos;

	GUIStyle buttonStyle;

	GUIStyle labelStyle;

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
		if(buttonStyle == null)
		{
			buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
		}

		if(labelStyle == null)
		{
			labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		}

		GUI.depth = 3;

		foreach(var info in helpInfos)
		{
			if(!info.isShow)
			{
				continue;
			}

			buttonStyle.fontSize = info.buttonFontSize;
			labelStyle.fontSize = info.labelFontSize;

			ShowGUI(
				info.area,
				info.buttonString,
				info.buttonSize,
				info.labelString,
				info.swap);
		}
	}

	void ShowGUI(Rect area, string button, float buttonSize, string label, bool swap)
	{		
		GUILayout.BeginArea(area);
		
		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();
		
		GUILayout.BeginVertical();

		GUILayout.FlexibleSpace();

		if(swap)
		{
			ShowLabel(label);
		}
		else
		{
			ShowButton(button, buttonSize);
		}

		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();
		
		if(swap)
		{
			ShowButton(button, buttonSize);
		}
		else
		{
			ShowLabel(label);
		}
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();
		
		GUILayout.EndHorizontal();
		
		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}

	void ShowButton(string button, float buttonSize)
	{
		GUILayout.Label(button, buttonStyle, GUILayout.MaxWidth(buttonSize), GUILayout.MinHeight(buttonSize));
	}

	void ShowLabel(string label)
	{
		GUILayout.Label(label, labelStyle);
	}
}
