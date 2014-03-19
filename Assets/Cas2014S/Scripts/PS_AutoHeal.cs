using UnityEngine;
using System.Collections;

public class PS_AutoHeal : PlayerSkill {

	public override SkillType Type {
		get {
			return SkillType.AutoHeal;
		}
	}

	public override string Name {
		get {
			return "自動回復";
		}
	}

	public override string Description {
		get {
			return "プレイヤースキル。徐々に体力が回復していく";
		}
	}

	public float healingSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
