using UnityEngine;
using System.Collections;

public class Fader : MyBehaviour {

	static Fader instance;
	public static Fader Instance{get{return instance;}set{}}

	Texture2D blackTexture;
	
	float alpha = 1.0f;

	bool abort = false;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
		blackTexture = new Texture2D(32,32,TextureFormat.RGB24,false);

		for(var y=0; y<32; ++y)
		{
			for(var x=0; x<32; ++x)
			{
				blackTexture.SetPixel(x, y, Color.black);
			}
		}

		blackTexture.Apply();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
//		if(!isFading)
//		{
//			return;
//		}
		
		GUI.color = new Color(0, 0, 0, alpha);

		GUI.depth = 2;
		
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture );
	}

	public void FadeOut(float time)
	{
		StartCoroutine(AsyncFade(alpha, 1.0f, time));
	}

	public void FadeIn(float time)
	{
		StartCoroutine(AsyncFade(alpha, 0.0f, time));
	}

	IEnumerator AsyncFade(float start, float end, float time)
	{
		alpha = start;

		var startTime = Time.time;
		
		while(Time.time - startTime < time)
		{
			if(abort)
			{
				break;
			}

			alpha = Mathf.Lerp(start, end, (Time.time - startTime) / time);
			//Debug.Log (alpha);
			
			yield return null;
		}

		alpha = end;
	}
}
