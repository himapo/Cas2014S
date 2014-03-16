using UnityEngine;
using System.Collections;

public class FlameThrower : PlayerGun {

	public override PlayerGunType Type {
		get {
			return PlayerGunType.FlameThrower;
		}
	}
	
	public override string Name {
		get {
			return "火炎放射器";
		}
	}
}
