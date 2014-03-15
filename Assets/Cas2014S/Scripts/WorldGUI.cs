using UnityEngine;
using System.Collections;

public class WorldGUITextInfo
{
	public string text;

	public Vector3 position;
}

public class WorldGUI : MonoBehaviour {

	static WorldGUI instance;
	public static WorldGUI Instance{get{return instance;}set{}}

	public GameObject damageTextPrefab;

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

	public void DrawText(WorldGUITextInfo info)
	{
		StartCoroutine(AsyncDrawText(info));
	}

	IEnumerator AsyncDrawText(WorldGUITextInfo info)
	{
		var text = Instantiate(damageTextPrefab, Vector3.zero, Quaternion.identity) as GameObject;

		text.GetComponent<GUIText>().text = info.text;

		var startTime = Time.time;

		while(Time.time - startTime < 1.0f)
		{
			var pos = Camera.main.WorldToViewportPoint(info.position);
			pos.z = 1.0f;
			text.GetComponent<GUIText>().transform.localPosition = pos;

			yield return null;
		}

		Destroy(text);
	}
}
