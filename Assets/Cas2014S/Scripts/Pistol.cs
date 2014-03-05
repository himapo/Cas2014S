using UnityEngine;
using System.Collections;

public class Pistol : PlayerGun {

	void Start()
	{
		magazineRemaining = magazineSize;
		
		AddSkill("GS_DamageUp");
		//AddSkill("GS_DamageUp");
		//AddSkill("GS_DamageUp");
		//AddSkill("PS_AutoHeal");
		//AddSkill("PS_AutoHeal");
	}

	protected override void Fire(Vector3 targetPosition)
	{
		var bulletDirection = targetPosition - muzzle.transform.position;
		bulletDirection.Normalize();
		
		SpawnBullet(bulletDirection);
		
		--magazineRemaining;
		
		PlayFireSound();
		
		ActivateMuzzleFlash();
	}
}
