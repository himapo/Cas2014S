using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MyBehaviour {

	PlayerGun[] guns = new PlayerGun[2];

	public GameObject[] weaponSlots;

	public string[] fireButtonNames = { "Fire1", "Fire2" };

	public string[] reloadButtonNames = { "Reload1", "Reload2" };

	public GameObject[] gunPrefabs;

	bool SwapTrigger{get{return PauseMenu.Instance.swapTrigger;}set{}}

	// Use this for initialization
	void Start () {
		//enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		CheckAccelerate();
		
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
				GetGun(SwapTrigger ? 1 - i : i).Shoot();
				//Debug.Log("SemiAutoShoot");
			}
			else if(Input.GetButton(fireButtonNames[i]))
			{
				GetGun(SwapTrigger ? 1 - i : i).ShootFullAuto();
				//Debug.Log("FullAutoShoot");
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

	void CheckAccelerate()
	{
		var timeScale = 1.0f;

		for(var i=0; i<2; ++i)
		{
			if(!Input.GetButton(fireButtonNames[SwapTrigger ? 1 - i : i]))
			{
				continue;
			}

			{
				var components = new List<GS_Accelerate>();

				components.AddRange(GetGun(i).GetComponents<GS_Accelerate>());

				foreach(var component in components)
				{
					timeScale *= component.timeScale;
				}
			}

			{
				var components = new List<GS_Decelerate>();

				components.AddRange(GetGun(i).GetComponents<GS_Decelerate>());

				foreach(var component in components)
				{
					timeScale *= component.timeScale;
				}
			}
		}

		Time.timeScale = timeScale;
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

	void OnGameOver()
	{
		enabled = false;
	}

	void OnPause()
	{
		enabled = false;
	}

	void OnUnpause()
	{
		enabled = true;
	}
}
