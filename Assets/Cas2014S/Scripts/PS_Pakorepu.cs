using UnityEngine;
using System.Collections;

public class PS_Pakorepu : PlayerSkill {

	public override SkillType Type {
		get {
			return SkillType.Pakorepu;
		}
	}
	
	public override string Name {
		get {
			return "壁抜け";
		}
	}
	
	public override string Description {
		get {
			return "プレイヤースキル。壁を通過することができる";
		}
	}

	void Awake()
	{
		rare = 3;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
