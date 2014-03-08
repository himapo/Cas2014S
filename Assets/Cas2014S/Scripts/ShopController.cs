using UnityEngine;
using System.Collections;

public class ShopController : MyBehaviour {
	
	public float useDistance = 1.5f;

	public GUIText openText;

	bool isShowOpen;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(!CameraRayCast.isHit || CameraRayCast.hit.distance > useDistance)
		{
			HideOpen();
			return;
		}

		var shopGUI = CameraRayCast.hit.collider.gameObject.GetComponent<ShopGUI>();

		if(shopGUI == null)
		{
			HideOpen();
			return;
		}

		if(shopGUI.isShopOpen)
		{
			HideOpen();
			return;
		}

		if(Input.GetButtonDown("Use"))
		{
			shopGUI.OpenShop();
			return;
		}

		ShowOpen();
	}

	void ShowOpen()
	{
		if(!isShowOpen)
		{
			isShowOpen = true;
			openText.text = "[E] Shop";
		}
	}

	void HideOpen()
	{
		if(isShowOpen)
		{
			isShowOpen = false;
			openText.text = "";
		}
	}
}
