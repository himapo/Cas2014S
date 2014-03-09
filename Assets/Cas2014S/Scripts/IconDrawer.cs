using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IconDrawer : MyBehaviour {

	public List<Color> contentColors;
	
	public List<Color> skillBackColors;

	public List<Color> gunBackColors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DrawSkill(Skill skill, Vector3 viewportPosition)
	{
		DrawIcon(
			skill.Name,
			contentColors[skill.rare],
			skillBackColors[(int)skill.Category],
			viewportPosition);
	}

	void DrawIcon(string content, Color contentColor, Color backgroundColor, Vector3 viewportPosition)
	{
		var screenPosition = Camera.main.ViewportToScreenPoint(viewportPosition);
		
		var scale = new Vector2((float)Screen.width / 1280.0f, (float)Screen.height / 720.0f);
		
		var iconSize = new Vector2(96.0f * scale.x, 32.0f * scale.y);		

		GUI.contentColor = contentColor;
		GUI.backgroundColor = backgroundColor;
		
		var originalStyle = GUI.skin.GetStyle("button");

		var guiStyle = new GUIStyle(originalStyle);
		guiStyle.alignment = TextAnchor.MiddleLeft;
		guiStyle.fontSize = 14;
		
		GUI.Label(
			new Rect(
				screenPosition.x - iconSize.x * 0.5f,
				screenPosition.y - iconSize.y * 0.5f,
				iconSize.x,
				iconSize.y),
			content,
			guiStyle);
	}
}
