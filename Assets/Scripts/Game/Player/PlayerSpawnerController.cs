using System;
using System.Linq;
using Game.Spawn;
using UnityEngine;

namespace Game
{
	public class PlayerSpawnerController : MonoBehaviour, ISpawner
	{		
		private PlayerSpawner[] _spawners;

		private void Awake()
		{
			_spawners = GetComponentsInChildren<PlayerSpawner>();
		}


		public event Action<int> OnCapacityChange;
		public int Capacity => _spawners.Sum(t => t.Capacity);

		public void Init(ISpawnCommand command)
		{			
			foreach (var spawner in _spawners)
			{
				spawner.Init(command);				
			}			
		}

		public void Set(int capacity, int level)
		{
			foreach (var spawner in _spawners)
			{				
				spawner.Set(capacity, level);
			}			
		}

		public void Spawn(Vector3 targetPosition)
		{
			var spawner = GetNearbySpawner(targetPosition);

			if (spawner.Capacity == 0)
			{
				return;
			}

			spawner.Spawn(targetPosition);				
			OnCapacityChange?.Invoke(Capacity);
		}

		public PlayerSpawner GetNearbySpawner(Vector3 targetPosition)
		{
			int nearestIndex = 0;
			while (_spawners[nearestIndex].Capacity == 0)
			{
				nearestIndex++;
			}
			for (int i = 1; i < _spawners.Length; i++)
			{
				if (((_spawners[i].transform.position - targetPosition).sqrMagnitude <
				     (_spawners[nearestIndex].transform.position - targetPosition).sqrMagnitude)
				    && _spawners[i].Capacity > 0)
				{
					nearestIndex = i;
				}
			}

			return _spawners[nearestIndex];
		}
	}
}
