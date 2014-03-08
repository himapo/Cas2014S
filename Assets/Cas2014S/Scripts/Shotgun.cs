using UnityEngine;
using System.Collections;

public class Shotgun : PlayerGun {

	public int bulletNumber = 8;

	public float scatterAngle = 10.0f;

	void Start()
	{
		magazineRemaining = magazineSize;

		AddSkill("GS_ReloadUp");
		AddSkill("GS_DamageUp");
		AddSkill("GS_ReloadUp");
		AddSkill("PS_AutoHeal");
		AddSkill("PS_AutoHeal");
		AddSkill("PS_AutoHeal");
		AddSkill("PS_AutoHeal");
	}

	protected override void Fire(Vector3 targetPosition)
	{
		for(var i=0; i<bulletNumber; ++i)
		{
			var scattered = ApplyScatter(targetPosition, scatterAngle);

			var bulletDirection = scattered - muzzle.transform.position;
			bulletDirection.Normalize();

			SpawnBullet(bulletDirection);
		}

		--magazineRemaining;

		PlayFireSound();

		ActivateMuzzleFlash();
	}
}
