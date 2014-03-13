using UnityEngine;
using System.Collections;

public class ShopController : MyBehaviour {
	
	public float useDistance = 1.5f;

	public ButtonHelp buttonHelp;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		buttonHelp.SetShow("shop", false);

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

		buttonHelp.SetShow("shop", true);
	}
}
