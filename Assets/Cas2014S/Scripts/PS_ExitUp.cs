using UnityEngine;
using System.Collections;

public class PS_ExitUp : PlayerSkill {

	public override SkillType Type {
		get {
			return SkillType.ExitUp;
		}
	}
	
	public override string Name {
		get {
			return "出口制圧強化";
		}
	}
	
	public override string Description {
		get {
			return "プレイヤースキル。次のフロアに進む速度が上がる";
		}
	}

	public float stayTimeScale = 0.8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
