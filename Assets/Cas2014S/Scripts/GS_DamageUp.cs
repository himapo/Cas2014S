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

	public override string Description {
		get {
			return "ガンスキル。ダメージが上昇する";
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
