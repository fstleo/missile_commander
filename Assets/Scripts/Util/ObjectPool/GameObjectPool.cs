using System.Collections.Generic;
using UnityEngine;

namespace Util.ObjectPool
{
	public class GameObjectPool : IObjectPool<GameObject> 
	{
		private readonly Stack<GameObject> _items;
		private readonly Transform _poolParent;

		private readonly GameObject _origin;

		public GameObjectPool(GameObject origin, int size)
		{
			_origin = origin;
			_items = new Stack<GameObject>(size);						
			_poolParent = new GameObject("Pool: " + origin.name).transform;			
			for (int i = 0; i < size; i++)
			{
				CreateNewItem();
			}			
		}

		private void CreateNewItem()
		{
			var newItem = Object.Instantiate(_origin);
			newItem.GetComponent<PoolItem>().Init(this);
			newItem.transform.SetParent(_poolParent);
			Return(newItem);
		}
		
		
		public GameObject Get()
		{
			if (_items.Count == 0)
			{
				Debug.LogWarning("Not enough items in pool, create a new one");
				CreateNewItem();
			}

			var item = _items.Pop();
			item.GetComponent<PoolItem>().ResetItem();
			return item;
		}

		public void Return(object item)
		{
			var cachedItem = item as GameObject;
			if (cachedItem != null)
			{
				Return(cachedItem);
			}
		}

		public void Return(GameObject item)
		{			
			item.SetActive(false);
			item.transform.SetParent(_poolParent);			
			_items.Push(item);	
		}

		object IObjectPool.Get()
		{
			return Get();
		}
	}
}
