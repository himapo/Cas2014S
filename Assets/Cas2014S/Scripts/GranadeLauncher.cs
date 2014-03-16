﻿using UnityEngine;
using System.Collections;

public class GranadeLauncher : PlayerGun {

	public override PlayerGunType Type {
		get {
			return PlayerGunType.GranadeLauncher;
		}
	}

	public override string Name {
		get {
			return "グレネードランチャー";
		}
	}

}
