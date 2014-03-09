using UnityEngine;
using System.Collections;

public class GunController : MyBehaviour {

	PlayerGun[] guns = new PlayerGun[2];

	public GameObject[] weaponSlots;

	public string[] fireButtonNames = { "Fire1", "Fire2" };

	public string[] reloadButtonNames = { "Reload1", "Reload2" };

	public GameObject[] gunPrefabs;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		for(var i=0; i < reloadButtonNames.Length; ++i)
		{
			if(Input.GetButtonDown(reloadButtonNames[i]))
			{
				GetGun(i).Reload();
			}
		}

		for(var i=0; i < fireButtonNames.Length; ++i)
		{
			if(Input.GetButtonDown(fireButtonNames[i]))
			{
				GetGun(i).Shoot();
			}
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log("change gun");
			ChangeGun(0);
		}

		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			Debug.Log("change shotgun");
			ChangeGun(1);
		}
	}

	public PlayerGun GetGunSafety(int index)
	{
		if(!guns[index])
		{
			guns[index] = weaponSlots[index].GetComponentInChildren<PlayerGun>();
		}

		return guns[index];
	}

	void ChangeGun(int type)
	{
		var gun = Instantiate(gunPrefabs[type]) as GameObject;
		gun.transform.parent = weaponSlots[0].transform;
		gun.transform.localPosition = Vector3.zero;
		gun.transform.localRotation = Quaternion.identity;
		Destroy(guns[0].gameObject);
		guns[0] = gun.GetComponent<PlayerGun>();
	}

	void OnShopOpen()
	{
		enabled = false;
	}
	
	void OnShopClose()
	{
		enabled = true;
	}
}
