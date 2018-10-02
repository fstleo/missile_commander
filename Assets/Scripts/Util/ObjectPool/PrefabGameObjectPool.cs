using UnityEngine;

namespace Util.ObjectPool
{
	[CreateAssetMenu(menuName = "Pool", fileName = "ItemPool")]
	public class PrefabGameObjectPool : ScriptableObject, IObjectPool<GameObject>
	{
		[SerializeField]
		private int _size;
	
		[SerializeField]
		private GameObject _origin;
	
		private GameObjectPool _pool;

		private GameObjectPool LazyPool
		{
			get
			{
				if (_pool == null)
				{
					_pool = new GameObjectPool(_origin, _size);
				}

				return _pool;
			}
		}

		object IObjectPool.Get()
		{
			return Get();
		}

		public void Return(GameObject item)
		{
			LazyPool.Return(item);
		}

		public GameObject Get()
		{
			return LazyPool.Get();
		}

		public void Return(object item)
		{
			Return((GameObject)item);
		}
	}
}
