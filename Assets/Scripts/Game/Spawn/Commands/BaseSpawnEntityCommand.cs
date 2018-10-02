using Common.Sound;
using Game.Components;
using Game.Spawn;
using UnityEngine;
using Util.ObjectPool;

public abstract class BaseSpawnEntityCommand : ISpawnCommand
{
	protected readonly Owner _owner;

	protected readonly IObjectPool<GameObject> _pool;
	
	protected BaseSpawnEntityCommand(IObjectPool<GameObject> pool, Owner owner)
	{
		_pool = pool;
		_owner = owner;
	}

	public virtual void Spawn(Vector3 from, Vector3 to, int level)
	{		
		SoundPlayer.Play("rocket_shot");
		var entity = _pool.Get();
		var moveComponent = entity.GetComponent<Move>();		
		moveComponent.Init( new []
			{ 
				from, 
				to 
			}, 
			level,
			ProcessReachTarget);					
	}

	protected abstract void ProcessReachTarget(GameObject gameObject, int num);
}
