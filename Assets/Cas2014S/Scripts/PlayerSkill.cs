using UnityEngine;
using System.Collections;

public abstract class PlayerSkill : Skill {

	public override SkillCategory Category {
		get {
			return SkillCategory.PlayerSkill;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
