using UnityEngine;
using System.Collections;

public class MissileBullet : GranadeBullet {

	public GameObject target;

	public float boostTime;

	public float boostImpulse;

	public float targetImpulse;

	// Use this for initialization
	void Start () {
		if(target == null)
		{
			target = Player;
		}

		rigidbody.AddForce(Vector3.up * boostImpulse, ForceMode.Impulse);

		StartCoroutine(AsyncHoming());
	}

	IEnumerator AsyncHoming()
	{
		var startTime = Time.time;

		while(Time.time - startTime < boostTime)
		{
			yield return null;
		}

		rigidbody.velocity = Vector3.zero;

		var targetDirection = GetTargetPosition() - transform.position;

		rigidbody.AddForce(targetDirection.normalized * targetImpulse, ForceMode.Impulse);	
	}

	Vector3 GetTargetPosition()
	{
		return target.transform.position;
	}
}
