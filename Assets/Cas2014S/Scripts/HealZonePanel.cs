using UnityEngine;
using System.Collections;

public class HealZonePanel : MonoBehaviour {
	
	public float rotationSpeed = 50.0f;
	
	float rotation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotation += rotationSpeed * Time.deltaTime;
		while(rotation > 360.0f)
		{
			rotation -= 360.0f;
		}
		
		transform.rotation = Quaternion.Euler(0, rotation, 0);
	}
}
