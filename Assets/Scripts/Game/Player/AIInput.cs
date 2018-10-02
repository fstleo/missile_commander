using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : MonoBehaviour
{

	[SerializeField] private Player _player;
	[SerializeField] private EntitiesWatcher _watcher;
//	private int _explosionLayerMask;

	private readonly HashSet<GameObject> _processedProjectiles = new HashSet<GameObject>();

	private void Awake()
	{
//		_explosionLayerMask = LayerMask.GetMask("Explosion");
		GameManager.OnStateChange += ListenToTheGameState;		
	}

	private void ListenToTheGameState(GameState state)
	{
		enabled = state == GameState.Start;
	}
	
	private void Update()
	{
		foreach (var projectile in _watcher.AliveEntitiesList)
		{
			if (_processedProjectiles.Contains(projectile))
			{
				continue;
			}
			_processedProjectiles.Add(projectile);			
			StartCoroutine(WatchForProjectile(projectile));
		}		
	}

	private IEnumerator WatchForProjectile(GameObject projectile)
	{
		Transform projectileTform = projectile.transform;
		Vector2 oldPosition = projectileTform.position;
		
		yield return null;
		yield return null;
		yield return null;
		
		Vector2 currentPosition = projectile.transform.position;
		Vector2 speedVector = currentPosition - oldPosition;

		if (_player.SpawnerController.Capacity == 0)
		{
			yield break;
		}
		projectile.GetComponent<PoolItem>().OnReturnToPool += ForgetProjectile; 
//		var raycast = Physics2D.Raycast(currentPosition + new Vector2(projectile.GetComponent<BoxCollider2D>().size.x / 2, 0),
//			projectileTform.right, 10, _explosionLayerMask);
//		if (raycast.transform != null)
//		{
//			var explosion = raycast.transform.GetComponent<Explosion>();
//			if (raycast.distance / (speedVector.magnitude / (3*Time.deltaTime)) < explosion.LifeTimeLeft && explosion.LifeTimeLeft < explosion.LifeTime / 2)
//			{								
//				yield break;
//			}
//		}
// 
		var time = (currentPosition - (Vector2)_player.SpawnerController.GetNearbySpawner(currentPosition)
			 .transform.position).magnitude / _player.ProjectileSpeed;		
		_player.SpawnProjectile(currentPosition + speedVector * time / (4*Time.deltaTime));		
	}

	private void ForgetProjectile(GameObject obj)
	{
		_processedProjectiles.Remove(obj);
	}
}
