using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExitCollision : MyBehaviour {

	public float stayTime = 3.0f;

	float elapsed;

	bool enter;

	// Use this for initialization
	void Start () {
		elapsed = 0;
		enter = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if(!enter)
		{
			return;
		}

		GUI.color = Color.green;

		var topleft = Camera.main.ViewportToScreenPoint(
			new Vector3(0.1f, 0.35f, 0.0f));
		
		var size = Camera.main.ViewportToScreenPoint(
			new Vector3(0.8f, 0.1f, 0.0f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		GUILayout.BeginArea(area);

		GUILayout.BeginHorizontal();
		
		GUILayout.FlexibleSpace();

		var labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		labelStyle.alignment = TextAnchor.MiddleCenter;
		labelStyle.fontSize = 24;

		GUILayout.Label(
			string.Format("次のフロアまであと{0:F2}秒", GetStayTime() - elapsed),
			labelStyle,
			GUILayout.MinWidth(360));

		GUILayout.FlexibleSpace();

		GUILayout.EndHorizontal();

		GUILayout.EndArea();
	}

	float GetStayTime()
	{
		var result = stayTime;
		var components = new List<PS_ExitUp>();
		components.AddRange(GetGun(0).GetComponents<PS_ExitUp>());
		components.AddRange(GetGun(1).GetComponents<PS_ExitUp>());
		foreach(var component in components)
		{
			result *= component.stayTimeScale;
		}
		return result;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			enter = true;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")
		{
			elapsed += Time.deltaTime;
		}

		if(enter && elapsed >= GetStayTime())
		{
			Debug.Log("Go to Next Floor");
			GameController.SendMessage("GotoNextFloor");
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
