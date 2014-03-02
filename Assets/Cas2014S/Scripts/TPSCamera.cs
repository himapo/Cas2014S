using UnityEngine;
using System.Collections;

public class TPSCamera : MonoBehaviour {

    [SerializeField]
    float distanceAway = 0.0f;

    [SerializeField]
    float distanceUp = 0.0f;

    [SerializeField]
    float smooth = 0.0f;

    [SerializeField]
    Transform follow;

    Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        follow = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;

        Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
        Debug.DrawRay(follow.position, -1f * follow.forward * distanceAway, Color.blue);
        Debug.DrawLine(follow.position, targetPosition, Color.magenta);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        transform.LookAt(follow);
    }
}
