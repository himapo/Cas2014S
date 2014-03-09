using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SkillStatus : MyBehaviour {

	IconDrawer iconDrawer;

	void Awake()
	{
		iconDrawer = GetComponent<IconDrawer>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		for(var i=0; i<2; ++i)
		{
			var gun = GetGun(i);
			
			foreach(var item in gun.skills.Select((val, index)=>{ return new {val, index};}))
			{
				var x = (i==0) ? 0.7f : 0.3f;
				if(item.index >=5)
				{
					x += ((i==0) ? -0.08f : 0.08f);
				}

				var y = 0.75f + 0.05f * item.index;
				if(item.index >= 5)
				{
					y -= 0.25f;
				}

				iconDrawer.DrawSkill(item.val, new Vector3(x, y, 0.0f));
			}

		}
	}
}
