using UnityEngine;
using System.Collections;

public class ShopController : MyBehaviour {
	
	public float useDistance = 1.5f;

	public Vector3 helpPosition;

	public Vector3 helpSize;
	
	bool isShowHelp = false;

	Rect helpArea;

	GUIStyle buttonStyle;

	GUIStyle labelStyle;

	// Use this for initialization
	void Start () {
		var topleft = Camera.main.ViewportToScreenPoint(helpPosition);
		
		var size = Camera.main.ViewportToScreenPoint(helpSize);
		
		helpArea = new Rect(
			topleft.x, topleft.y,
			size.x, size.y);
	}
	
	// Update is called once per frame
	void Update () {

		isShowHelp = false;

		if(!CameraRayCast.isHit || CameraRayCast.hit.distance > useDistance)
		{
			return;
		}

		var shopGUI = CameraRayCast.hit.collider.gameObject.GetComponent<ShopGUI>();

		if(shopGUI == null)
		{
			return;
		}

		if(shopGUI.isShopOpen)
		{
			return;
		}

		if(Input.GetButtonDown("Use"))
		{
			shopGUI.OpenShop();
			return;
		}

		isShowHelp = true;
	}

	void OnGUI()
	{
		if(!isShowHelp)
		{
			return;
		}

		if(buttonStyle == null)
		{
			buttonStyle = new GUIStyle(GUI.skin.GetStyle("button"));
			buttonStyle.fontSize = 20;
		}

		if(labelStyle == null)
		{
			labelStyle = new GUIStyle(GUI.skin.GetStyle("label"));
			labelStyle.fontSize = 30;
		}

		GUILayout.BeginArea(helpArea);

		GUILayout.FlexibleSpace();
		
		GUILayout.BeginHorizontal();

		GUILayout.BeginVertical();
		
		GUILayout.FlexibleSpace();

		GUILayout.Button("E", buttonStyle, GUILayout.MaxWidth(50), GUILayout.MinHeight(50));

		GUILayout.FlexibleSpace();
		
		GUILayout.EndVertical();

		GUILayout.BeginVertical();

		GUILayout.FlexibleSpace();

		GUILayout.Label("ショップ", labelStyle);

		GUILayout.FlexibleSpace();

		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();
		
		GUILayout.EndArea();
	}
}
