using UnityEngine;
using Util.ObjectPool;

public class ReturnToPoolOnDestroy : PoolItem
{
	private IDestroyable _destroyableComponent;

	public override void Init(IObjectPool<GameObject> pool)
	{
		base.Init(pool);
		_destroyableComponent = GetComponent<IDestroyable>();
		_destroyableComponent.OnDestroy += ReturnOnDestroy;		
	}

	private void ReturnOnDestroy(Owner owner)
	{
		Return();		
	}

	public override void ResetItem()
	{
		base.ResetItem();
		_destroyableComponent.Reset();
	}
}
