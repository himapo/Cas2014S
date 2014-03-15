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
		enabled = false;
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
	}

	public PlayerGun GetGunSafety(int index)
	{
		if(!guns[index])
		{
			guns[index] = weaponSlots[index].GetComponentInChildren<PlayerGun>();
		}

		return guns[index];
	}

	public void ChangeGun(int gunIndex, PlayerGunType type)
	{
		var gun = Instantiate(gunPrefabs[(int)type]) as GameObject;
		gun.transform.parent = weaponSlots[gunIndex].transform;
		gun.transform.localPosition = Vector3.zero;
		gun.transform.localRotation = Quaternion.identity;

		var oldGun = guns[gunIndex];
		var newGun = gun.GetComponent<PlayerGun>();

		foreach(var skill in oldGun.skills)
		{
			newGun.AddSkill(skill.Type);
		}

		Destroy(guns[gunIndex].gameObject);
		guns[gunIndex] = newGun;
	}

	void OnShopOpen()
	{
		enabled = false;
	}
	
	void OnShopClose()
	{
		enabled = true;
	}

	void OnPauseMenuOpen()
	{
		enabled = false;
	}
	
	void OnPauseMenuClose()
	{
		enabled = true;
	}

	void OnBeginFloorMove()
	{
		enabled = false;
	}

	void OnEndFloorMove()
	{
		enabled = true;
	}

	void OnGotoTitle()
	{
		enabled = false;
	}

	void OnGameClear()
	{
		enabled = false;
	}

	void OnPickWindowOpen()
	{
		enabled = false;
	}
	
	void OnPickWindowClose()
	{
		enabled = true;
	}
}
