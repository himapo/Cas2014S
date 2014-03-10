using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconDrawer : MyBehaviour {

	static IconDrawer instance;
	public static IconDrawer Instance{get{return instance;}set{}}

	public List<Color> contentColors;
	
	public List<Color> skillBackColors;

	public List<Color> gunBackColors;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawGun(PlayerGun gun)
	{
		DrawShopIcon(
			gun.Name,
			contentColors[gun.rare],
			gunBackColors[0],
			Vector3.zero, true);
	}

	public void DrawStatusSkill(Skill skill, Vector3 viewportPosition, bool layout)
	{
		DrawStatusIcon(
			skill.Name,
			contentColors[skill.rare],
			skillBackColors[(int)skill.Category],
			viewportPosition, layout);
	}

	public void DrawShopSkill(Skill skill, Vector3 viewportPosition, bool layout)
	{
		DrawShopIcon(
			skill.Name,
			contentColors[skill.rare],
			skillBackColors[(int)skill.Category],
			viewportPosition, layout);
	}

	void DrawStatusIcon(string content, Color contentColor, Color backgroundColor, Vector3 viewportPosition, bool layout)
	{
		var backupContentColor = GUI.contentColor;
		var backupBackgroundColor = GUI.backgroundColor;

		GUI.contentColor = contentColor;
		GUI.backgroundColor = backgroundColor;
		
		var originalStyle = GUI.skin.GetStyle("button");

		var guiStyle = new GUIStyle(originalStyle);
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 14;

		if(layout)
		{
			GUILayout.Label(content, guiStyle);
		}
		else
		{
			var screenPosition = Camera.main.ViewportToScreenPoint(viewportPosition);
			
			var scale = new Vector2((float)Screen.width / 1280.0f, (float)Screen.height / 720.0f);
			
			var iconSize = new Vector2(96.0f * scale.x, 32.0f * scale.y);		

			GUI.Label(
				new Rect(
					screenPosition.x - iconSize.x * 0.5f,
					screenPosition.y - iconSize.y * 0.5f,
					iconSize.x,
					iconSize.y),
				content,
				guiStyle);
		}

		GUI.contentColor = backupContentColor;
		GUI.backgroundColor = backupBackgroundColor;
	}

	void DrawShopIcon(string content, Color contentColor, Color backgroundColor, Vector3 viewportPosition, bool layout)
	{
		var backupContentColor = GUI.contentColor;
		var backupBackgroundColor = GUI.backgroundColor;

		GUI.contentColor = contentColor;
		GUI.backgroundColor = backgroundColor;
		
		var originalStyle = GUI.skin.GetStyle("button");

		var guiStyle = new GUIStyle(originalStyle);
		guiStyle.alignment = TextAnchor.MiddleCenter;
		guiStyle.fontSize = 14;
		
		if(layout)
		{
			GUILayout.Label(
				content
			    ,guiStyle
				,GUILayout.MinWidth(150)
				//,GUILayout.MinHeight(30)
				);
		}
		else
		{
			var screenPosition = Camera.main.ViewportToScreenPoint(viewportPosition);
			
			var scale = new Vector2((float)Screen.width / 1280.0f, (float)Screen.height / 720.0f);
			
			var iconSize = new Vector2(96.0f * scale.x, 32.0f * scale.y);		
			
			GUI.Label(
				new Rect(
				screenPosition.x - iconSize.x * 0.5f,
				screenPosition.y - iconSize.y * 0.5f,
				iconSize.x,
				iconSize.y),
				content,
				guiStyle);
		}

		GUI.contentColor = backupContentColor;
		GUI.backgroundColor = backupBackgroundColor;
	}
}
