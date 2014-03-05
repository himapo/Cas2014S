using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	public Texture[] textures;

	GunController gunController;

	// Use this for initialization
	void Start () {
		var player = GameObject.FindWithTag("Player");
		gunController = player.GetComponent<GunController>();
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
		var gun = gunController.guns[gunIndex] as PlayerGun;
		return gun.GetRecoil();
	}
}
