﻿using UnityEngine;
using System.Collections;

public class Pistol : PlayerGun {

	public override PlayerGunType Type {
		get {
			return PlayerGunType.Pistol;
		}
	}

	public override string Name {
		get {
			return "ピストル";
		}
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