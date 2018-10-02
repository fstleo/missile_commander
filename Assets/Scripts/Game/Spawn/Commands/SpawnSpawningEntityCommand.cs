using Game.Components;
using UnityEngine;
using Util.ObjectPool;

namespace Game.Spawn.Commands
{
	public class SpawnSpawningEntityCommand : SpawnSplittingEntityCommand
	{
		public SpawnSpawningEntityCommand(IObjectPool<GameObject> pool, Vector3 [] targets, Owner owner, ISpawnCommand spawnOthersCommand) : base(pool, targets,owner, spawnOthersCommand) { }
	
		public override void Spawn(Vector3 from, Vector3 to, int level)
		{
			_level = level;		
			var entity = _pool.Get();
			var moveComponent = entity.GetComponent<Move>();
		
			Vector3 [] movePositions = new Vector3[level + 2];
			for (int i = 0; i < movePositions.Length; i++)
			{
				movePositions[i] = Vector3.Lerp(from, to, (1f * i) / (level + 1));
			}
		
			moveComponent.Init(movePositions, level, ProcessReachTarget);			
		}

		protected override void ProcessReachTarget(GameObject gameObject, int num)
		{
			if (_level + 1 == num)
			{
				gameObject.GetComponent<PoolItem>().Return();
				return;
			}

			SpawnOthers(gameObject.transform.position);
		}
	}
}
