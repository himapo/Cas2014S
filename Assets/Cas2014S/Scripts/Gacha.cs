using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Gacha : MyBehaviour {

	List<GachaItem> itemTable = new List<GachaItem>();

	[HideInInspector]
	public List<GachaItem> drawnItems = new List<GachaItem>();

	void Awake()
	{
		itemTable.AddRange(GetComponents<GachaItem>());
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Draw(int n)
	{
		drawnItems.Clear();

		var probSum = itemTable.Sum((param)=>{
			return param.probability;
		});

		for(var i=0; i<n; ++i)
		{
			DrawOne (probSum);
		}
	}

	void DrawOne(int probSum)
	{
		var sample = Random.Range(0, probSum);
		
		var border = 0;
		var item = itemTable.First((param)=>{
			border += param.probability;
			return border > sample;
		});
		
		drawnItems.Add(item);
	}
}
