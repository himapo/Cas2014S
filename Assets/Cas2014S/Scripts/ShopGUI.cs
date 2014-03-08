using UnityEngine;
using System.Collections;

public class ShopGUI : MyBehaviour {

	[HideInInspector]
	public bool isShopOpen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(!isShopOpen)
		{
			return;
		}

		if(Input.GetButtonDown("Fire2"))
		{
			CloseShop();
		}
	}

	void OnGUI()
	{
		if(!isShopOpen)
		{
			return;
		}

		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), " ");

		var screenPosition = Camera.main.ViewportToScreenPoint(
			new Vector3(0.5f, 0.2f, 0.0f));

		if(GUI.Button(
			new Rect(screenPosition.x, screenPosition.y, 80.0f, 40.0f),
	        "Buy"))
		{
			Debug.Log("Buy");
		}
	}

	public void OpenShop()
	{
		if(isShopOpen)
		{
			return;
		}

		isShopOpen = true;

		Screen.lockCursor = false;
		Screen.showCursor = true;

		Player.SendMessage("OnShopOpen", SendMessageOptions.DontRequireReceiver);

		var enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(var enemy in enemys)
		{
			enemy.SendMessage("OnShopOpen", SendMessageOptions.DontRequireReceiver);
		}
	}

	void CloseShop()
	{
		if(!isShopOpen)
		{
			return;
		}

		isShopOpen = false;

		Screen.lockCursor = true;
		Screen.showCursor = false;

		Player.SendMessage("OnShopClose", SendMessageOptions.DontRequireReceiver);
		
		var enemys = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(var enemy in enemys)
		{
			enemy.SendMessage("OnShopClose", SendMessageOptions.DontRequireReceiver);
		}
	}
}
