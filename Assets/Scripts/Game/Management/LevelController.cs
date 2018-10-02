using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField]
	private EntitiesWatcher _watcher;
	
	[SerializeField]
	private EnemyPlayer _enemyPlayer;

	private void Update()
	{
		if (!_enemyPlayer.CanSpawn && !_watcher.IsSomethingInside)
		{
			GameManager.LevelUp();
		}
	}

}
