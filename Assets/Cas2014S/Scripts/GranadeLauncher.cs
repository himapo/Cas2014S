using UnityEngine;
using System.Collections;

public class GranadeLauncher : Pistol {

	public override PlayerGunType Type {
		get {
			return PlayerGunType.GranadeLauncher;
		}
	}

}
