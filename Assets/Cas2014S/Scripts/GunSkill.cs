using UnityEngine;
using System.Collections;

public abstract class GunSkill : Skill {

	public override SkillCategory Category {
		get {
			return SkillCategory.GunSkill;
		}
	}

	[HideInInspector]
	public GunBase gun;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
