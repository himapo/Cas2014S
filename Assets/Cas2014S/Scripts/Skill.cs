﻿using UnityEngine;
using System.Collections;

public enum SkillCategory
{
	GunSkill,
	PlayerSkill,
	Max,
}

public enum SkillType
{
	// Gun Skills
	DamageUp,
	ReloadUp,
	Absorb,
	Accuracy,

	// Player Skills
	AutoHeal,
	JumpUp,
	ExitUp,
	SpeedUp,

	Max,
}

public abstract class Skill : MyBehaviour {

	public abstract SkillCategory Category{get;}

	public abstract SkillType Type { get; }

	public abstract string Name{get;}

	public abstract string Description{get;}

	public int rare;

	public static string GetSkillName(SkillType type)
	{
		switch(type)
		{
		case SkillType.DamageUp:
			return "GS_DamageUp";
		case SkillType.ReloadUp:
			return "GS_ReloadUp";
		case SkillType.AutoHeal:
			return "PS_AutoHeal";
		case SkillType.JumpUp:
			return "PS_JumpUp";
		case SkillType.ExitUp:
			return "PS_ExitUp";
		case SkillType.SpeedUp:
			return "PS_SpeedUp";
		case SkillType.Absorb:
			return "GS_Absorb";
		case SkillType.Accuracy:
			return "GS_Accuracy";
		default:
			break;
		}

		return "";
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
