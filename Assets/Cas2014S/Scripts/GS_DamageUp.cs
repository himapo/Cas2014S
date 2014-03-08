using UnityEngine;
using System.Collections;

public class GS_DamageUp : GunSkill {

	public override SkillType Type {
		get {
			return SkillType.DamageUp;
		}
	}

	public override string Name {
		get {
			return "ダメージUP";
		}
	}

	public float damageScale = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
