using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunBase : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float bulletImpulse = 300.0f;

	public bool raycastBullet;

	public Transform muzzle;

	List<Skill> skills = new List<Skill>();

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
		var damageUps = GetComponents<GS_DamageUp>();
		foreach(var damageUp in damageUps)
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

	public void AddSkill(string skillName)
	{
		if(skills.Count == maxSkillSlot)
		{
			RemoveSkill(0);
		}

		var skill = gameObject.AddComponent(skillName) as Skill;
		var gunSkill = skill as GunSkill;
		if(gunSkill != null)
		{
			gunSkill.gun = this;
		}
		skills.Add(skill);
	}

	public void RemoveSkill(int index)
	{
		if(index >= skills.Count)
		{
			return;
		}

		Destroy(skills[index]);
		skills.RemoveAt(index);
	}
}
