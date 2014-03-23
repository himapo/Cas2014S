using UnityEngine;
using System.Collections;

public class GS_Absorb : GunSkill {

	public override SkillType Type {
		get {
			return SkillType.Absorb;
		}
	}
	
	public override string Name {
		get {
			return "体力吸収";
		}
	}
	
	public override string Description {
		get {
			return "ガンスキル。ダメージを与えると体力が回復する";
		}
	}
	
	public float absorbPercentage = 5.0f;

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
