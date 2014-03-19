using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class FloorItem
{
	public GameObject prefab;

	public int probability;
}

public class FloorGenerator : MyBehaviour {

	static FloorGenerator instance;

	public static FloorGenerator Instance{get{return instance;}set{}}

	public float wallProbability;

	public float gridSize = 3.0f;
	
	public int[] gridNum = new int[2]{ 10, 10 };

	public GameObject exitPrefab;

	public GameObject shopPrefab;

	public GameObject wallPrefab;

	public int minEnemy;
	
	public int maxEnemy;

	public int minItem;
	
	public int maxItem;

	public List<FloorItem> itemTable;

	List<int> emptyGridNumbers = new List<int>();

	List<int> usedGridNumbers = new List<int>();

	int floor;

	void Awake()
	{
		instance = this;

		//gridEmptyFlags.AddRange(Enumerable.Repeat(true, gridNum[0] * gridNum[1]));

		emptyGridNumbers.AddRange(Enumerable.Range(0, gridNum[0] * gridNum[1]));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate(int floor)
	{
		this.floor = floor;

		emptyGridNumbers.AddRange(usedGridNumbers);
		usedGridNumbers.Clear();

		SetPlayerPosition();

		//GenerateWall();

		SpawnExit();

		SpawnShop();

		SpawnItems ();

		SpawnEnemys ();
	}

	int SampleRandomEmptyGrid()
	{
		var index = Random.Range(0, emptyGridNumbers.Count);
		return emptyGridNumbers[index];
	}

	void MarkGridUsed(int gridIndex)
	{
		if(!emptyGridNumbers.Contains(gridIndex))
		{
			return;
		}
		emptyGridNumbers.Remove(gridIndex);
		usedGridNumbers.Add(gridIndex);
	}

	void SpawnObject(GameObject prefab, int gridIndex, float margin, Quaternion rotation)
	{
		var position = GetGridPosition(gridIndex);

		Instantiate(
			prefab,
			new Vector3(
				position.x,
				prefab.transform.position.y,
				position.z),
			rotation);

		MarkGridUsed(gridIndex);
	}

	Vector3 GetGridPosition(int gridIndex)
	{
		return new Vector3(
			gridSize * (gridIndex % gridNum[0] - gridNum[0] / 2) + gridSize * 0.5f,
			0.0f,
			gridSize * (gridIndex / gridNum[0] - gridNum[1] / 2) + gridSize * 0.5f);
	}

	void SetPlayerPosition()
	{
		var grid = SampleRandomEmptyGrid();
		var position = GetGridPosition(grid);
		
		Player.transform.position = new Vector3(
			position.x,
			2.0f,
			position.z);
		
		MarkGridUsed(grid);
	}

	void SpawnExit()
	{
		SpawnObject(exitPrefab, SampleRandomEmptyGrid(), 0, Quaternion.identity);
	}

	void SpawnShop()
	{
		SpawnObject(shopPrefab, SampleRandomEmptyGrid(), 0, Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up));
	}

	void SpawnEnemys()
	{
		var n = Random.Range(minEnemy, maxEnemy);
		
		for(var i=0; i<n; ++i)
		{
			SpawnEnemy();
		}
	}

	void SpawnEnemy()
	{
		var gridIndex = SampleRandomEmptyGrid();
		EnemyGenerator.Instance.Generate(floor, GetGridPosition(gridIndex));
		MarkGridUsed(gridIndex);
	}

	void SpawnItems()
	{
		var n = Random.Range(minItem, maxItem);

		var probSum = itemTable.Sum((item)=>{
			return item.probability;
		});

		for(var i=0; i<n; ++i)
		{
			SpawnItem(probSum);
		}
	}

	void SpawnItem(int probSum)
	{
		var sample = Random.Range(0, probSum);
		
		var border = 0;
		var item = itemTable.First((param)=>{
			border += param.probability;
			return border > sample;
		});

		SpawnObject(item.prefab, SampleRandomEmptyGrid(), 0, Quaternion.identity);
	}

	void GenerateWall()
	{
		// 壁から隣接している部屋リストへのマップ
		var wallChamberMap = new Dictionary<int, List<int>>();
		// 部屋から壁リストへのマップ
		var chamberWallMap = new Dictionary<int, List<int>>();

		// 垂直方向の壁
		for(var y=0; y<gridNum[1]; ++y)
		{
			for(var x=0; x<gridNum[0]-1; ++x)
			{
				var wall = y * (gridNum[0] - 1) + x;

				var chamberList = new List<int>();
				var chamberA = wall + y;
				var chamberB = wall + 1 + y;
				chamberList.Add(chamberA);
				chamberList.Add(chamberB);

				wallChamberMap.Add(wall, chamberList);

				if(!chamberWallMap.ContainsKey(chamberA))
				{
					chamberWallMap.Add(chamberA, new List<int>());
				}
				if(!chamberWallMap.ContainsKey(chamberB))
				{
					chamberWallMap.Add(chamberB, new List<int>());
				}
				chamberWallMap[chamberA].Add(wall);
				chamberWallMap[chamberB].Add(wall);
			}
		}

		var verticalWallNum = wallChamberMap.Count;

		// 水平方向の壁
		for(var y=0; y<gridNum[1]-1; ++y)
		{
			for(var x=0; x<gridNum[0]; ++x)
			{
				var wall = y * gridNum[0] + x + verticalWallNum;
				
				var chamberList = new List<int>();
				var chamberA = y * gridNum[0] + x;
				var chamberB = (y+1) * gridNum[0] + x;
				chamberList.Add(chamberA);
				chamberList.Add(chamberB);
				
				wallChamberMap.Add(wall, chamberList);
				
				if(!chamberWallMap.ContainsKey(chamberA))
				{
					chamberWallMap.Add(chamberA, new List<int>());
				}
				if(!chamberWallMap.ContainsKey(chamberB))
				{
					chamberWallMap.Add(chamberB, new List<int>());
				}
				chamberWallMap[chamberA].Add(wall);
				chamberWallMap[chamberB].Add(wall);
			}
		}

		while(chamberWallMap.Count > 1)
		{
			var wallIndex = Random.Range(0, wallChamberMap.Count);

			var wall = wallChamberMap.Keys.ElementAt(wallIndex);

			if(wallChamberMap[wall].Count == 1)
			{
				var chamber = wallChamberMap[wall][0];

				chamberWallMap[chamber].Remove(wall);
				
				wallChamberMap.Remove(wall);
			}
			else if(wallChamberMap[wall].Count == 2)
			{
				var chamberA = wallChamberMap[wall][0];
				var chamberB = wallChamberMap[wall][1];

				chamberWallMap[chamberA].Remove(wall);
				chamberWallMap[chamberB].Remove(wall);
				chamberWallMap[chamberA].AddRange(chamberWallMap[chamberB]);
				chamberWallMap[chamberA] = chamberWallMap[chamberA].Distinct().ToList();

				foreach(var wallB in chamberWallMap[chamberB])
				{
					var wallBChambers = wallChamberMap[wallB];

					wallBChambers.Remove(chamberB);

					if(!wallBChambers.Contains(chamberA))
					{
						wallBChambers.Add(chamberA);
					}
				}

				chamberWallMap.Remove(chamberB);
				wallChamberMap.Remove(wall);
			}
		}

		foreach(var wall in wallChamberMap.Keys)
		{
			var gridIndex = 0;
			var vertical = true;

			if(wall < (gridNum[0] - 1) * gridNum[1])
			{
				// 垂直壁
				var y = wall / (gridNum[0] - 1);
				gridIndex = wall + y;

				vertical = true;
			}
			else
			{
				// 水平壁
				gridIndex = wall - (gridNum[0] - 1) * gridNum[1];

				vertical = false;
			}

			SpawnWall(gridIndex, vertical);
		}
	}

	void GenerateWallOld()
	{
		for(var x=0; x<gridNum[0] - 1; ++x)
		{
			if(Random.value < wallProbability)
			{
				var length = Random.Range(1, 10);

				var wallStart = Random.Range(0, gridNum[1] - length);

				for(var y=wallStart; y<wallStart+length; ++y)
				{
					SpawnWall(CalcGridIndex(x, y), true);
				}
			}
		}

		for(var y=0; y<gridNum[1] - 1; ++y)
		{
			if(Random.value < wallProbability)
			{
				var length = Random.Range(1, 10);
				
				var wallStart = Random.Range(0, gridNum[0] - length);
				
				for(var x=wallStart; x<wallStart+length; ++x)
				{
					SpawnWall(CalcGridIndex(x, y), false);
				}
			}
		}
	}

	int CalcGridIndex(int x, int y)
	{
		return gridNum[0] * y + x;
	}

	void SpawnWall(int grid, bool vertical)
	{
		var position = GetGridPosition(grid);

		Instantiate(
			wallPrefab,
			new Vector3(
				position.x + gridSize * (vertical ? 0.5f : 0.0f),
				wallPrefab.transform.position.y,
				position.z + gridSize * (vertical ? 0.0f : 0.5f)),
			Quaternion.AngleAxis(vertical ? 90.0f : 0.0f, Vector3.up));
	}
}
