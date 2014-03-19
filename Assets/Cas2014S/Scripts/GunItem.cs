using UnityEngine;
using System.Collections;

public class GunItem : MyBehaviour {

	public PlayerGun gun;

	bool isShowPick;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if(!isShowPick)
		{
			return;
		}

		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0.35f, 0.2f, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(0.3f, 0.4f));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);

		GUILayout.BeginArea(area, "", "box");
		{
			GUILayout.BeginHorizontal();
			{
				//GUILayout.FlexibleSpace();
			
				var labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
				labelStyle.alignment = TextAnchor.MiddleCenter;
				labelStyle.fontSize = 30;

				GUILayout.Label(
					"銃を拾った！",
					labelStyle);

				//GUILayout.FlexibleSpace();
			}	
			GUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal("box", GUILayout.MinHeight(100));
			{
				GUILayout.BeginVertical();
				{
		
					GUILayout.FlexibleSpace();
		
					if(GUILayout.Button("左手に装備"))
					{
						GunController.ChangeGun(0, gun.Type);
						KillMe();
					}
					
					GUILayout.FlexibleSpace();
		
				}
				GUILayout.EndVertical();
		
				GUILayout.FlexibleSpace();

				GUILayout.BeginVertical();
				{
					GUILayout.FlexibleSpace();
				
					IconDrawer.Instance.DrawGun(gun);
				
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndVertical();

				GUILayout.FlexibleSpace();

				GUILayout.BeginVertical();
				{
					GUILayout.FlexibleSpace();
					
					if(GUILayout.Button("右手に装備"))
					{
						GunController.ChangeGun(1, gun.Type);
						KillMe();
					}
					
					GUILayout.FlexibleSpace();
				}
				GUILayout.EndVertical();
		
			}
			GUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			{
				GUILayout.FlexibleSpace();
				
				var buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
				buttonStyle.fontSize = 16;

				if(GUILayout.Button("いらない", buttonStyle, GUILayout.MinHeight(50)))
				{
					isShowPick = false;
					BroadcastAll("OnPickWindowClose");
				}
				
				GUILayout.FlexibleSpace();
			}
			GUILayout.EndHorizontal();
		}
		GUILayout.EndArea();

		ToolTip();
	}

	void ToolTip()
	{
		var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 15;
		
		var topleft = Camera.main.ViewportToScreenPoint(new Vector3(0.2f, 0.12f, 0));
		
		var size = Camera.main.ViewportToScreenPoint(new Vector3(0.6f, 0.1f, 0));
		
		var area = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
		
		GUI.Label(area, GUI.tooltip, guiStyle);
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.tag == "Player")
		{
			ButtonHelp.Instance.SetShow("pick", false);
		}
	}

	void OnTriggerStay(Collider collider)
	{
		if(isShowPick)
		{
			return;
		}

		if(collider.tag != "Player")
		{
			return;
		}

		if(Input.GetButton("Use"))
		{
			isShowPick = true;
			BroadcastAll("OnPickWindowOpen");
			ButtonHelp.Instance.SetShow("pick", false);
			return;
		}

		ButtonHelp.Instance.SetShow("pick", true);
	}

	void KillMe()
	{
		isShowPick = false;
		BroadcastAll("OnPickWindowClose");
		ButtonHelp.Instance.SetShow("pick", false);
		Destroy(gameObject);
	}

	void OnBeginFloorMove()
	{
		ButtonHelp.Instance.SetShow("pick", false);
		Destroy(gameObject);
	}

	void OnGotoTitle()
	{
		ButtonHelp.Instance.SetShow("pick", false);
		Destroy(gameObject);
	}

	void OnPauseMenuOpen()
	{
		enabled = false;
	}

	void OnPauseMenuClose()
	{
		enabled = true;
		isShowPick = false;
	}
}
