using UnityEngine;
using System.Collections;

public enum ItemType
{
	Gun,
	Skill,
	
	Max,
}

public class GachaItem : MyBehaviour {

	public ItemType type;

	public int probability;
	
	public PlayerGun gun;
	
	public Skill skill;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
