using UnityEngine;
using System.Collections;

public class PS_JumpUp : PlayerSkill {

	public override SkillType Type {
		get {
			return SkillType.JumpUp;
		}
	}
	
	public override string Name {
		get {
			return "ジャンプ力UP";
		}
	}
	
	public override string Description {
		get {
			return "プレイヤースキル。ジャンプ力が上昇する";
		}
	}
	
	public float jumpSpeed = 1.0f;

	void Awake()
	{
		rare = 1;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
