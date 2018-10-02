using System.Collections.Generic;
using UnityEngine;
using Util.ObjectPool;

namespace Game.Spawn.Commands
{
	public class SpawnSplittingEntityCommand : SpawnEntityCommand
	{
		private readonly ISpawnCommand _spawnOthersCommand;
		protected int _level;
		private readonly Vector3 [] _targets;

		public SpawnSplittingEntityCommand(IObjectPool<GameObject> pool, Vector3 [] targets,  Owner owner, ISpawnCommand spawnOthersCommand) : base(pool,owner)
		{
			_targets = targets;
			_spawnOthersCommand = spawnOthersCommand;
		}
	
		public override void Spawn(Vector3 from, Vector3 to, int level)
		{
			_level = level;
			base.Spawn(from, from + (to - from)/ Random.Range(2f,4f), level);
		}

		protected override void ProcessReachTarget(GameObject gameObject, int num)
		{
			SpawnOthers(gameObject.transform.position);
			gameObject.GetComponent<PoolItem>().Return();
		}

		protected void SpawnOthers(Vector3 position)
		{
			HashSet<int> positions = new HashSet<int>();
			int splitCount = Random.Range(2, Mathf.Max(2, _level - 1));

			while (positions.Count < splitCount)
			{
				positions.Add(Random.Range(0, _targets.Length));
			}
		
			foreach (var index in positions)
			{
				_spawnOthersCommand.Spawn(position, _targets[index], _level);
			}
		}
	}
}
