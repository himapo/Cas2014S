using UnityEngine;
using System.Collections;

public class Shotgun : PlayerGun {

	public override PlayerGunType Type {
		get {
			return PlayerGunType.Shotgun;
		}
	}

	public int bulletNumber = 8;

	public float scatterAngle = 10.0f;

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
