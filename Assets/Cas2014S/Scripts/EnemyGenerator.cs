using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class EnemyControllerSpec
{
	public int probability;

	public bool chasePlayer;
	
	public bool runAway;
	
	public bool randomWalk;

	public float movementSpeed;

	public float rotationRatio;

	public float closeDistance;
}

[System.Serializable]
public class EnemyGunSpec
{
	public int probability;
	
	public GameObject bulletPrefab;

	public float bulletImpulse;

	public bool raycastBullet;

	public float fireInterval;

	public float startDistance;

	public float bulletDamage;

	public float explosionDamage;
}

[System.Serializable]
public class EnemySpecTable
{
	public int startFloor;

	public List<EnemyControllerSpec> controllerSpecs;

	public List<EnemyGunSpec> gunSpecs;
}

public class EnemyGenerator : MyBehaviour {

	static EnemyGenerator instance;
	public static EnemyGenerator Instance{get{return instance;}set{}}

	public List<EnemySpecTable> floorEnemySpecs;

	public GameObject enemyPrefab;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate(int floor, Vector3 position)
	{
		var enemy = Instantiate(enemyPrefab, position, Quaternion.identity) as GameObject;

		SetControllerSpec(floor, enemy);
		SetGunSpec(floor, enemy);
	}

	void SetControllerSpec(int floor, GameObject enemy)
	{
		var specStartFloor = floorEnemySpecs.Where((floorSpec)=>{
			return floorSpec.controllerSpecs.Count > 0 && floorSpec.startFloor <= floor;
		}).Max((floorSpec)=>{
			return floorSpec.startFloor;
		});
		
		var specs = floorEnemySpecs.FirstOrDefault((floorSpec)=>{
			return floorSpec.startFloor == specStartFloor;
		}).controllerSpecs;

		var probSum = specs.Sum((s)=>{
			return s.probability;
		});

		var sample = Random.Range(0, probSum);

		var border = 0;
		var spec = specs.First((s)=>{
			border += s.probability;
			return border > sample;
		});

		var enemyController = enemy.GetComponent<EnemyController>();
		enemyController.chasePlayer = spec.chasePlayer;
		enemyController.runAway = spec.runAway;
		enemyController.randomWalk = spec.randomWalk;
		enemyController.movementSpeed = spec.movementSpeed;
		enemyController.rotationRatio = spec.rotationRatio;
		enemyController.closeDistance = spec.closeDistance;
	}

	void SetGunSpec(int floor, GameObject enemy)
	{
		var specStartFloor = floorEnemySpecs.Where((floorSpec)=>{
			return floorSpec.gunSpecs.Count > 0 && floorSpec.startFloor <= floor;
		}).Max((floorSpec)=>{
			return floorSpec.startFloor;
		});

		var specs = floorEnemySpecs.FirstOrDefault((floorSpec)=>{
			return floorSpec.startFloor == specStartFloor;
		}).gunSpecs;
		
		var probSum = specs.Sum((s)=>{
			return s.probability;
		});
		
		var sample = Random.Range(0, probSum);
		
		var border = 0;
		var spec = specs.First((s)=>{
			border += s.probability;
			return border > sample;
		});
		
		var enemyGun = enemy.GetComponent<EnemyGun>();
		enemyGun.bulletPrefab = spec.bulletPrefab;
		enemyGun.bulletImpulse = spec.bulletImpulse;
		enemyGun.raycastBullet = spec.raycastBullet;
		enemyGun.interval = spec.fireInterval;
		enemyGun.bulletDamage = spec.bulletDamage;
		enemyGun.startDistance = spec.startDistance;
		enemyGun.explosionDamage = spec.explosionDamage;
	}
}
