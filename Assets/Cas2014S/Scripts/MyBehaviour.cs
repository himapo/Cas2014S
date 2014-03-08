using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyBehaviour : MonoBehaviour {

	GameObject player;

	protected GameObject Player
	{
		get
		{
			if(player == null)
			{
				player = GameObject.FindWithTag("Player");
			}

			return player;
		}
		set
		{
		}
	}

	protected Health PlayerHealth
	{
		get
		{
			return Player.GetComponent<Health>();
		}
		set
		{
		}
	}
	
	protected PlayerController PlayerController
	{
		get
		{
			return Player.GetComponent<PlayerController>();
		}
		set
		{
		}
	}

	protected GunController GunController
	{
		get
		{
			return Player.GetComponent<GunController>();
		}
		set
		{
		}
	}

	protected GunBase GetGun(int index)
	{
		return GunController.guns[index];
	}

	protected IEnumerable<T> GetGunComponents<T>()
		where T : Component
	{
		var result = new List<T>();
		result.AddRange(GetGun (0).GetComponents<T>());
		result.AddRange(GetGun (1).GetComponents<T>());
		return result;
	}

	protected CameraRayCast CameraRayCast
	{
		get
		{
			return Player.GetComponent<CameraRayCast>();
		}
		set
		{
		}
	}
}
