using UnityEngine;
using System.Collections;

public class GS_Accuracy : GunSkill {

	public override SkillType Type {
		get {
			return SkillType.Accuracy;
		}
	}
	
	public override string Name {
		get {
			return "集弾性能UP";
		}
	}
	
	public override string Description {
		get {
			return "ガンスキル。弾のバラつきが抑えられる";
		}
	}

	public float recoilScale = 0.8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
