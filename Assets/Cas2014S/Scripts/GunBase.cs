using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunBase : MyBehaviour
{
    public GameObject bulletPrefab;

    public float bulletImpulse;

	public bool raycastBullet;

	public Transform muzzle;

	[HideInInspector]
	public List<Skill> skills = new List<Skill>();

	public int maxSkillSlot;

	public float bulletDamage;

	public float soundInterval;

	float soundTime;

	protected Vector3 GetCameraTarget()
	{
		var camera = Camera.main;
		
		var cameraRay = new Ray(camera.transform.position, camera.transform.forward);

		RaycastHit cameraHit;
		Vector3 result;

		LayerMask mask = -1;
		
		mask &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
		mask &= ~(1 << LayerMask.NameToLayer("Bullet"));
		mask &= ~(1 << LayerMask.NameToLayer("Zone"));

		var isHit = Physics.Raycast(cameraRay, out cameraHit, Mathf.Infinity, mask);

		var targetDirection = cameraHit.point - muzzle.transform.position;
		targetDirection.Normalize();

		// 射撃方向が後ろ
		var backward = Vector3.Dot(targetDirection, muzzle.transform.forward) < 0.0f;

		if(!isHit || backward)
		{
			// 銃口方向に飛ばす
			result = muzzle.transform.position + muzzle.transform.forward * 100000.0f;
		}
		else
		{
			// ターゲットの方向に飛ばす
			result = cameraHit.point;
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
		bulletComponent.shooter = transform.root.gameObject;
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

		if(Time.time - soundTime < soundInterval)
		{
			return;
		}

		soundTime = Time.time;

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

	public void AddSkill(SkillType type)
	{
		AddSkill(Skill.GetSkillName(type));
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
