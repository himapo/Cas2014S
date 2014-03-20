using UnityEngine;
using System.Collections;

public class PS_SpeedUp : PlayerSkill {

	public override SkillType Type {
		get {
			return SkillType.SpeedUp;
		}
	}
	
	public override string Name {
		get {
			return "移動速度UP";
		}
	}
	
	public override string Description {
		get {
			return "プレイヤースキル。移動速度が上昇する";
		}
	}

	public float movementSpeed = 1.0f;

	void Awake()
	{
		rare = 0;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
