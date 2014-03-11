using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletHitInfo
{
    public Ray Ray { get; set; }

    public RaycastHit Hit { get; set; }

	public Vector3 HitPosition { get; set; }

    public float Damage { get; set; }
}

public class Health : MyBehaviour {

    public int maxHealth = 100;

    public float healingSpeed = 0.0f;

    [HideInInspector]
    public int health;

	// 回復のために小数点以下を保持
	float innerHealth;

	GameObject worldGUI;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		innerHealth = health;
		worldGUI = GameObject.FindWithTag("WorldGUI");
	}
	
	// Update is called once per frame
	void Update () {

        if (health > 0)
        {
			if(health < maxHealth)
			{
	            innerHealth += (GetHealingSpeed() * Time.deltaTime);
           		health = Mathf.FloorToInt(Mathf.Clamp(innerHealth, 0, (float)maxHealth));
			}
        }
        else
        {
            gameObject.SendMessage(
				"OnDie",
				SendMessageOptions.DontRequireReceiver);
        }
	}

    public void OnBulletHit(BulletHitInfo info)
    {
		Debug.Log(string.Format("{0} Damage", gameObject.name));;

		var damageInt = Mathf.FloorToInt(info.Damage);

        health -= damageInt;

		if(health < maxHealth)
		{
        	health = Mathf.Clamp(health, 0, maxHealth); 
		}
		innerHealth = health;

		worldGUI.SendMessage(
			"DrawText",
			new WorldGUITextInfo()
			{
				text = damageInt.ToString(),
				position = info.HitPosition,
			});
    }

	float GetHealingSpeed()
	{
		var result = healingSpeed;

		var autoHeals = GetGunComponents<PS_AutoHeal>();

		foreach(var autoHeal in autoHeals)
		{
			result += autoHeal.healingSpeed;
		}

		return result;
	}

	public void AddHealth(int amount)
	{
		health += amount;
		health = Mathf.Clamp(health, 0, maxHealth); 
		innerHealth = health;
	}

	public void AddMaxHealth(int amount)
	{
		maxHealth += amount;
		maxHealth = Mathf.Clamp(maxHealth, 0, int.MaxValue); 
	}
}
