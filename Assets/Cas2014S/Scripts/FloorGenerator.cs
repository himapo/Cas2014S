using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FloorGenerator : MyBehaviour {

	static FloorGenerator instance;

	public static FloorGenerator Instance{get{return instance;}set{}}

	//List<GameObject> generatedObjects = new List<GameObject>();

	public GameObject exitPrefab;

	public GameObject shopPrefab;

	public GameObject wallPrefab;

	public float gridSize = 3.0f;

	public int[] gridNum = new int[2]{ 10, 10 };

	//List<bool> gridEmptyFlags = new List<bool>();

	List<int> emptyGridNumbers = new List<int>();

	List<int> usedGridNumbers = new List<int>();

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

	public void Generate()
	{
		emptyGridNumbers.AddRange(usedGridNumbers);
		usedGridNumbers.Clear();

		SetPlayerPosition();

		GenerateWall();

		SpawnExit();

		SpawnShop();
	}

	int SampleRandomEmptyGrid()
	{
		var index = Random.Range(0, emptyGridNumbers.Count);
		return emptyGridNumbers[index];
	}

	void MarkGridUsed(int gridIndex)
	{
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
		var grid = SampleRandomEmptyGrid();
		SpawnObject(exitPrefab, grid, 0, Quaternion.identity);
		MarkGridUsed(grid);
	}

	void SpawnShop()
	{
		var grid = SampleRandomEmptyGrid();
		SpawnObject(shopPrefab, grid, 0, Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up));
		MarkGridUsed(grid);
	}

	void SpawnEnemy()
	{
	}

	void SpawnItem()
	{
	}

	void GenerateWall()
	{
		for(var x=0; x<gridNum[0] - 1; ++x)
		{
			if(Random.value < 0.2f)
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
			if(Random.value < 0.2f)
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
