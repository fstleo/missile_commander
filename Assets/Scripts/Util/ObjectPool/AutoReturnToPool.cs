using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.ObjectPool
{
	public class AutoReturnToPool<T> : MonoBehaviour where T : CachedMonoBehaviour
	{
		private float _timer = 0;
		private IObjectPool<T> _pool;
		private T _item;

		public void Init(IObjectPool<T> pool, T item, float time)
		{
			_timer = time;
			_item = item;
		}

		private void Update()
		{
			_timer -= Time.deltaTime;
			if (_timer < 0)
			{
				_pool.Return(_item);
				Destroy(this);
			}

		}

	}
}