﻿using UnityEngine;
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
	public float innerHealth;

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

    public int OnBulletHit(BulletHitInfo info)
    {
		//Debug.Log(string.Format("{0} Damage", gameObject.name));;

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

		gameObject.SendMessage(
			"OnDamage",
			SendMessageOptions.DontRequireReceiver);

		return damageInt;
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

	public int Heal(float percentage)
	{
		if(health < maxHealth)
		{
			var heal = Mathf.FloorToInt(percentage * maxHealth / 100.0f);
			heal = Mathf.Min (heal, maxHealth - health);
			health += heal;
			health = Mathf.Clamp(health, 0, maxHealth);
			innerHealth = health;
			return heal;
		}
		return 0;
	}

	public int Heal(int amount)
	{
		if(health < maxHealth)
		{
			var heal = amount;
			heal = Mathf.Min (heal, maxHealth - health);
			health += heal;
			health = Mathf.Clamp(health, 0, maxHealth);
			innerHealth = health;
			return heal;
		}
		return 0;
	}

	public void AddHealth(int amount)
	{
		health += amount;
		if(health < maxHealth)
		{
			health = Mathf.Clamp(health, 0, maxHealth); 
		}
		innerHealth = health;
	}

	public void AddMaxHealth(int amount)
	{
		maxHealth += amount;
		maxHealth = Mathf.Clamp(maxHealth, 0, int.MaxValue); 
	}
}
