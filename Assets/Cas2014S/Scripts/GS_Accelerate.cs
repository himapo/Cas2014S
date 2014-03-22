using UnityEngine;
using System.Collections;

public class GS_Accelerate : GunSkill {

	public override SkillType Type {
		get {
			return SkillType.Accelerate;
		}
	}
	
	public override string Name {
		get {
			return "加速装置";
		}
	}
	
	public override string Description {
		get {
			return "ガンスキル。トリガーを引くと反応速度が加速し、主観時間が遅くなる";
		}
	}

	public float timeScale = 0.7f;

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
