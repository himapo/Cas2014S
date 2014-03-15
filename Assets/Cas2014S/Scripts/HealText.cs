using UnityEngine;
using System.Collections;

public class HealText : MonoBehaviour {

	public float speed;

	public float lifeSpan;

	[HideInInspector]
	public int heal;

	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		GetComponent<GUIText>().text = string.Format("+{0}", heal);
	}

	// Update is called once per frame
	void Update () {

		transform.position += Vector3.up * speed * Time.deltaTime;
		
		if(Time.time - startTime > lifeSpan)
		{
			Destroy(gameObject);
		}
	}
}
