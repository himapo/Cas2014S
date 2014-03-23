using UnityEngine;
using System.Collections;

public class ShopController : MyBehaviour {
	
	public float useDistance = 3.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		ButtonHelp.Instance.SetShow("shop", false);

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

		ButtonHelp.Instance.SetShow("shop", true);
	}
}
