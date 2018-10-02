using Common.Sound;
using Game.Components;
using UnityEngine;
using Util.ObjectPool;

namespace Game.Spawn.Commands
{
	public class SpawnEntityCommand : BaseSpawnEntityCommand
	{
	
		public SpawnEntityCommand(IObjectPool<GameObject> pool, Owner owner) : base (pool,owner) { }

		protected override void ProcessReachTarget(GameObject gameObject, int num)
		{					
			gameObject.GetComponent<IDestroyable>().Destroy(_owner);
		}
	}
}
