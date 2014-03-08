using UnityEngine;
using System.Collections;

public enum SkillType
{
	DamageUp,
	ReloadUp,

	AutoHeal,
}

public abstract class Skill : MonoBehaviour {

	public abstract SkillType Type { get; }

	public abstract string Name{get;}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
