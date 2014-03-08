using UnityEngine;
using System.Collections;
using System.Linq;

public class SkillStatus : MyBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		var originalStyle = GUI.skin.GetStyle("box");
		
		var iconSize = new Vector2(80.0f, 24.0f);

		for(var i=0; i<2; ++i)
		{
			var gun = GunController.guns[i];
			
			foreach(var item in gun.skills.Select((val, index)=>{ return new {val, index};}))
			{
				var guiStyle = new GUIStyle(originalStyle);

				guiStyle.alignment = TextAnchor.MiddleLeft;

				var x = (i==0) ? 0.7f : 0.3f;
				if(item.index >=5)
				{
					x += ((i==0) ? -0.07f : 0.07f);
				}

				var y = 0.75f + 0.05f * item.index;
				if(item.index >= 5)
				{
					y -= 0.25f;
				}

				var screenPosition = Camera.main.ViewportToScreenPoint(
					new Vector3(x, y, 0.0f));

				GUI.Box(
					new Rect(
						screenPosition.x - iconSize.x * 0.5f,
				        screenPosition.y - iconSize.y * 0.5f,
						iconSize.x,
						iconSize.y),
				    item.val.Name,
					guiStyle);
			}

		}
	}
}
