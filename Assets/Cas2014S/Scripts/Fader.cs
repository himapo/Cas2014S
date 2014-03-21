using UnityEngine;
using System.Collections;

public class Fader : MyBehaviour {

	static Fader instance;
	public static Fader Instance{get{return instance;}set{}}

	Texture2D blackTexture;

	Texture2D redTexture;
	
	float blackAlpha = 1.0f;

	float redAlpha = 0.0f;
	
	bool abort = false;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
		blackTexture = new Texture2D(32,32,TextureFormat.RGB24,false);
		redTexture = new Texture2D(32,32,TextureFormat.RGB24,false);
		
		for(var y=0; y<32; ++y)
		{
			for(var x=0; x<32; ++x)
			{
				blackTexture.SetPixel(x, y, Color.black);
				redTexture.SetPixel(x, y, Color.red);
			}
		}

		blackTexture.Apply();
		redTexture.Apply();

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

		GUI.depth = 2;

		if(blackAlpha > 0.0f)
		{
			GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
			GUI.color = new Color(0, 0, 0, blackAlpha);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture );
			GUI.EndGroup();
		}

		if(redAlpha > 0.0f)
		{
			GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
			GUI.color = new Color(1, 0, 0, redAlpha);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), redTexture );
			GUI.EndGroup();
		}
	}

	public void FadeOut(float time)
	{
		StartCoroutine(AsyncFadeBlack(blackAlpha, 1.0f, time));
	}

	public void FadeIn(float time)
	{
		StartCoroutine(AsyncFadeBlack(blackAlpha, 0.0f, time));
	}

	public void RedFadeIn(float time)
	{
		StartCoroutine(AsyncFadeRed(0.5f, 0.0f, time));
	}

	IEnumerator AsyncFadeBlack(float start, float end, float time)
	{
		blackAlpha = start;

		var startTime = Time.time;
		
		while(Time.time - startTime < time)
		{
			if(abort)
			{
				break;
			}

			blackAlpha = Mathf.Lerp(start, end, (Time.time - startTime) / time);
			//Debug.Log (alpha);
			
			yield return null;
		}

		blackAlpha = end;
	}

	IEnumerator AsyncFadeRed(float start, float end, float time)
	{
		redAlpha = start;
		
		var startTime = Time.time;
		
		while(Time.time - startTime < time)
		{
			if(abort)
			{
				break;
			}
			
			redAlpha = Mathf.Lerp(start, end, (Time.time - startTime) / time);
			//Debug.Log (alpha);
			
			yield return null;
		}
		
		redAlpha = end;
	}
}
