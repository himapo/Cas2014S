using UnityEngine;
using System.Collections;

public class PauseCounter : MyBehaviour {

	static PauseCounter instance;
	public static PauseCounter Instance{get{return instance;}set{}}

	GUIText counterText;

	int counter;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		counterText = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		counterText.text = string.Format("counter {0}", counter);
	}

	public void Increment()
	{
		if(counter++ == 0)
		{
			BroadcastAll("OnPause");
		}
	}

	public void Decrement()
	{
		if(--counter == 0)
		{
			BroadcastAll("OnUnpause");
		}

		if(counter < 0)
		{
			Debug.LogError(string.Format("PauseCounter {0}", counter));
		}
	}

	public void Unpause()
	{
		counter = 0;
		//BroadcastAll("OnUnpause");
	}
}
