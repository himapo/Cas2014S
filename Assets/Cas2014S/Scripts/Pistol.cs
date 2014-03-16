using UnityEngine;
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
}
