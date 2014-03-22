using UnityEngine;
using System.Collections;

public class GS_FastInterval : GunSkill {

	public override SkillType Type {
		get {
			return SkillType.FastInterval;
		}
	}
	
	public override string Name {
		get {
			return "連射速度上昇";
		}
	}
	
	public override string Description {
		get {
			return "ガンスキル。弾の発射間隔が短くなる";
		}
	}
	
	public float intervalScale = 0.8f;

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
