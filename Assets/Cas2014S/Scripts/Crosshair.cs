using UnityEngine;
using System.Collections;

public class Crosshair : MyBehaviour {

	public Texture[] textures;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		Draw(0);
		Draw(1);
	}

	void Draw(int gunIndex)
	{
		var recoil = GetRecoil(gunIndex) * 6.0f;

		var pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);

		GUIUtility.RotateAroundPivot(0.0f + 45.0f * gunIndex, pivotPoint);

		var defaultSpace = 16;

		var texture = textures[gunIndex];

		// left
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 - defaultSpace - recoil, pivotPoint.y - 16, texture.width, texture.height), texture);
		// right
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 + defaultSpace + recoil, pivotPoint.y - 16, texture.width, texture.height), texture);
		
		GUIUtility.RotateAroundPivot(90.0f, pivotPoint);
		
		// top
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 - defaultSpace - recoil, pivotPoint.y - 16, texture.width, texture.height), texture);
		// bottom
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 + defaultSpace + recoil, pivotPoint.y - 16, texture.width, texture.height), texture);
	}

	float GetRecoil(int gunIndex)
	{
		var gun = GetGun (gunIndex) as PlayerGun;
		return gun.GetRecoil();
	}
}
