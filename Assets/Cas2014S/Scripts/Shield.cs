using UnityEngine;
using System.Collections;

public class Shield : PlayerGun {
	
	public MeshRenderer shieldMeshRenderer;

	public CapsuleCollider shieldCollider;

	bool isOpen;
		
	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	protected override void Update () {

		shieldMeshRenderer.enabled = isOpen;
		shieldCollider.enabled = isOpen;
		
		if(isOpen && !isPause)
		{
			--magazineRemaining;
		}

		base.Update();
			
		if(IsReloading())
		{
			isOpen = false;
		}
	}

	protected override void Fire(Vector3 targetPosition)
	{
		isOpen = true;
	}

	public override void Reload ()
	{
		// 弾切れになるまでリロード不可
		if(magazineRemaining == 0)
		{
			base.Reload ();
		}
	}
}
