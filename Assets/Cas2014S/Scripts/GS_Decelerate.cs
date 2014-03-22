using UnityEngine;
using System.Collections;

public class GS_Decelerate : GunSkill {
	
	public override SkillType Type {
		get {
			return SkillType.Decelerate;
		}
	}
	
	public override string Name {
		get {
			return "減速装置";
		}
	}
	
	public override string Description {
		get {
			return "ガンスキル。トリガーを引くと反応速度が減速し、主観時間が速くなる";
		}
	}
	
	public float timeScale = 1.43f;

	void Awake()
	{
		rare = 2;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
