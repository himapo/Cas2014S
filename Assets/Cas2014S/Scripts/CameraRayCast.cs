using UnityEngine;
using System.Collections;

public class CameraRayCast : MyBehaviour {

	[HideInInspector]
	public bool isHit;

	[HideInInspector]
	public RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var camera = Camera.main;
		
		var cameraRay = new Ray(camera.transform.position, camera.transform.forward);
		
		LayerMask mask = -1;
		
		mask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
		mask &= ~(1 << LayerMask.NameToLayer("Bullet"));
		mask &= ~(1 << LayerMask.NameToLayer("Zone"));
		
		isHit = Physics.Raycast(cameraRay, out hit, Mathf.Infinity, mask);
	}
}
