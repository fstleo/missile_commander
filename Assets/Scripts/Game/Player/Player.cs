using System.Collections;
using System.Collections.Generic;
using Common.Sound;
using Game;
using Game.Entities;
using Game.Spawn.Commands;
using UnityEngine;
using Util.ObjectPool;

public class Player : MonoBehaviour
{
	[SerializeField]
	private PlayerSpawnerController _spawnerController;

	[SerializeField]
	private GameObject _projectile;

	[SerializeField]
	private float _minY = -3;

	public PlayerSpawnerController SpawnerController => _spawnerController;
	
	public int ProjectileSpeed { get; private set; }

	private void Awake()
	{
		GameManager.OnLevelChange += ProcessLevelChange;		
	}	

	private void ProcessLevelChange(int level)
	{
		ProjectileSpeed = GameManager.Settings.PlayerProjectileSpeed;
		SpawnerController.Set(GameManager.Settings.PlayerSpawnerCapacity, ProjectileSpeed);
	}

	private void Start()
	{
		var projectilePool = new GameObjectPool(_projectile, 30);
		var spawnCommand = new SpawnEntityCommand(projectilePool, Owner.Player);
		SpawnerController.Init(spawnCommand);		
	}

	public void SpawnProjectile(Vector3 position)
	{
		if (SpawnerController.Capacity > 0)
		{
			SpawnerController.Spawn(new Vector3(position.x, Mathf.Max(_minY, position.y)));
		}
		else
		{
			SoundPlayer.Play("cant_shoot");
		}

	}

}
