using UnityEngine;
using System.Collections;

public class GunStatus : MonoBehaviour {

	public GunController gunController;

	public GUIText[] guiTexts;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		for(var i=0; i<2; ++i)
		{
			if(guiTexts.Length < 2)
			{
				// 要素数が少ないときがある・・・？
				break;
			}

			var gun = gunController.guns[i];

			if(gun.IsReloading())
			{
				guiTexts[i].text = "Reloading...";
			}
			else
			{
				guiTexts[i].text = string.Format(
					"{0}/{1}",
					gun.magazineRemaining,
            	    gun.magazineSize);
			}
		}

	}
}
