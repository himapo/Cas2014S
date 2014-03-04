using UnityEngine;
using System.Collections;

public class Pistol : PlayerGun {

	protected override void Fire(Vector3 targetPosition)
	{
		var bulletDirection = targetPosition - muzzle.transform.position;
		bulletDirection.Normalize();
		
		var bullet = SpawnBullet(bulletDirection);
		
		--magazineRemaining;
		
		PlayFireSound();
		
		ActivateMuzzleFlash();
	}
}
