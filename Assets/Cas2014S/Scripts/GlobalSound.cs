using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GlobalSoundID
{
	KillEnemy,

	Max,
}

public class GlobalSound : MyBehaviour {

	static GlobalSound instance;
	public static GlobalSound Instance{get{return instance;}set{}}

	public List<AudioSource> audioSources;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(int index)
	{
		if(index >= audioSources.Count)
		{
			return;
		}

		audioSources[index].Play ();
	}
}
