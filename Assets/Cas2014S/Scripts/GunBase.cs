using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunBase : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletImpulse = 300.0f;

	public bool raycastBullet;

	public Transform muzzle;

	List<GunSkill> gunSkills = new List<GunSkill>();

	public int maxSkillSlot = 5;

	public float bulletDamage = 3.0f;

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
		bulletComponent.bulletDamage = GetBulletDamage();

		return bullet;
	}

	public float GetBulletDamage()
	{
		var result = bulletDamage;
		var damageUp = GetComponent<GS_DamageUp>();
		if(damageUp != null)
		{
			result *= damageUp.damageScale;
		}
		return result;
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

	public void AddGunSkill(string skillName)
	{
		if(gunSkills.Count == maxSkillSlot)
		{
			RemoveGunSkill(0);
		}

		var gunSkill = gameObject.AddComponent(skillName) as GunSkill;
		gunSkill.gun = this;
		gunSkills.Add(gunSkill);
	}

	public void RemoveGunSkill(int index)
	{
		if(index >= gunSkills.Count)
		{
			return;
		}

		Destroy(gunSkills[index]);
		gunSkills.RemoveAt(index);
	}
}
