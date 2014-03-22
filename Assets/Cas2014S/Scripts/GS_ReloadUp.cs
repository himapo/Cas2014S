using UnityEngine;
using System.Collections;

public class GS_ReloadUp : GunSkill {

	public override SkillType Type {
		get {
			return SkillType.ReloadUp;
		}
	}

	public override string Name {
		get {
			return "リロードUP";
		}
	}

	public override string Description {
		get {
			return "ガンスキル。リロード時間が短縮される";
		}
	}

	public float reloadSpeedScale = 1.2f;

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
