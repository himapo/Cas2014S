using UnityEngine;
using System.Collections;

public class Shotgun : PlayerGun {

	public int bulletNumber = 8;

	public float scatterAngle = 10.0f;

	protected override void Fire(Vector3 targetPosition)
	{
		var centerDirection = targetPosition - muzzle.transform.position;;
		centerDirection.Normalize();

		var axis = Vector3.Cross(centerDirection, Vector3.up);
		axis.Normalize();

		for(var i=0; i<bulletNumber; ++i)
		{
			// 銃口を原点とみなす
			var targetPoint = centerDirection;

			// 中心軸から5度ずらす
			targetPoint = Quaternion.AngleAxis(Random.value * scatterAngle, axis) * targetPoint;

			// 中心軸を中心に円状に分布
			targetPoint = Quaternion.AngleAxis(Random.value * 360.0f, centerDirection) * targetPoint;

			var bulletDirection = targetPoint;
			bulletDirection.Normalize();

			SpawnBullet(bulletDirection);
		}

		--magazineRemaining;

		PlayFireSound();

		ActivateMuzzleFlash();
	}
}
