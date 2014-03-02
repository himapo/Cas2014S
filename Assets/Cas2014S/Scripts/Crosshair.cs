using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	public Texture texture;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		var pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);

		// left
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 - 32, pivotPoint.y - 16, texture.width, texture.height), texture);
		// right
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 + 32, pivotPoint.y - 16, texture.width, texture.height), texture);

		GUIUtility.RotateAroundPivot(90, pivotPoint);

		// top
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 - 32, pivotPoint.y - 16, texture.width, texture.height), texture);
		// bottom
		GUI.DrawTexture(new Rect(pivotPoint.x - 16 + 32, pivotPoint.y - 16, texture.width, texture.height), texture);
	}
}
