using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyBehaviour : MonoBehaviour {

	GameController gameController;

	protected GameController GameController
	{
		get
		{
			if(gameController == null)
			{
				gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
			}

			return gameController;
		}
		set
		{
		}
	}

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

	protected PlayerGun GetGun(int index)
	{
		return GunController.GetGunSafety(index);
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

	protected void BroadcastAll(string methodName)
	{
		foreach(var gameObject in GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[])
		{
			if(gameObject != null && gameObject.transform.parent == null)
			{
				gameObject.gameObject.BroadcastMessage(methodName, null, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	protected void ResetPlayer(GameObject player)
	{
		this.player = player;
	}
}
