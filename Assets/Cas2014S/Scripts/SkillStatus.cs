using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillStatus : MyBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		GUI.depth = 3;

		for(var i=0; i<2; ++i)
		{
			var gun = GetGun(i);

			for(var index=0; index<gun.maxSkillSlot; ++index)
			{
				var x = (i==0) ? 0.3f : 0.7f;
				if(index >=5)
				{
					x += ((i==0) ? 0.08f : -0.08f);
				}
				
				var y = 0.75f + 0.05f * index;
				if(index >= 5)
				{
					y -= 0.25f;
				}

				if(index < gun.skills.Count)
				{
					IconDrawer.Instance.DrawStatusSkill(gun.skills[index], new Vector3(x, y, 0.0f), false);
				}
				else
				{
					IconDrawer.Instance.DrawStatusSkillEmpty(new Vector3(x, y, 0.0f), false);
				}
			}
			
//			foreach(var item in gun.skills.Select((val, index)=>{ return new {val, index};}))
//			{
//				var x = (i==0) ? 0.3f : 0.7f;
//				if(item.index >=5)
//				{
//					x += ((i==0) ? 0.08f : -0.08f);
//				}
//
//				var y = 0.75f + 0.05f * item.index;
//				if(item.index >= 5)
//				{
//					y -= 0.25f;
//				}
//
//				IconDrawer.Instance.DrawStatusSkill(item.val, new Vector3(x, y, 0.0f), false);
//			}

		}
	}
}
