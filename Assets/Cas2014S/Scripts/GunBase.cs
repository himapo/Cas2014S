using UnityEngine;
using System.Collections;

public class GunBase : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletImpulse = 300.0f;

	public bool raycastBullet;

	public Transform muzzle;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

	protected Vector3 GetCameraTarget()
	{
		var camera = Camera.main;
		
		var cameraRay = new Ray(camera.transform.position, camera.transform.forward);

		RaycastHit cameraHit;
		Vector3 result;

		if(Physics.Raycast(cameraRay, out cameraHit))
		{
			result = cameraHit.point;
		}
		else
		{
			result = muzzle.transform.position + muzzle.transform.forward * 100000.0f;
		}

		return result;
	}

	protected GameObject SpawnBullet(Vector3 direction)
	{
		var bullet = Instantiate(
			bulletPrefab,
		    muzzle.transform.position,
			muzzle.transform.rotation) as GameObject;
		bullet.rigidbody.AddForce(direction * bulletImpulse, ForceMode.Impulse);

		var bulletComponent = bullet.GetComponent<Bullet>();
		bulletComponent.raycastBullet = raycastBullet;
		bulletComponent.direction = direction;
		bulletComponent.shooter = gameObject;

		return bullet;
	}

	protected virtual void PlayFireSound()
	{
		var audio = GetComponent<AudioSource>();

		if(audio == null)
		{
			return;
		}

		audio.Play();
	}

	protected virtual void ActivateMuzzleFlash()
	{
		StartCoroutine(AsyncMuzzleFlash());
	}

	IEnumerator AsyncMuzzleFlash()
	{
		var light = muzzle.gameObject.GetComponent<Light>();
		
		if(light == null){
			yield break;
		}

		var startTime = Time.time;
		light.intensity = 4.0f;
		
		while(Time.time - startTime < 0.01f)
		{
			yield return null;
		}

		light.intensity = 0.0f;
	}
}
