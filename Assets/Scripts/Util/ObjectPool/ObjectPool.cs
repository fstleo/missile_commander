using System.Collections.Generic;
using UnityEngine;

namespace Util.ObjectPool
{
	public class CachedMonoBehaviourPool<T> : IObjectPool<T> where T: CachedMonoBehaviour
	{
		private readonly Stack<T> _objects;
		private readonly Transform _poolParent;

		private readonly GameObject _origin;

		public CachedMonoBehaviourPool(GameObject origin, int size)
		{
			_objects = new Stack<T>(size);			
			_origin = origin;
			_poolParent = new GameObject("Pool: " + origin.name).transform;
			
			for (int i = 0; i < size; i++)
			{
				CreateNewItem();
			}
		}

		private T CreateNewItem()
		{
			var newItem = Object.Instantiate(_origin, _poolParent).GetComponent<T>();			
			_objects.Push(newItem);
			return newItem;
		}
		
		
		public T Get(float time = -1)
		{
			if (_objects.Count == 0)
			{
				Debug.LogWarning("Not enough item in pool, add one item");
				CreateNewItem();
			}

			var item = _objects.Pop();
			item.CachedGameObject.SetActive(true);
			item.CachedTransform.SetParent(null);
			if (time > -0.5f)
			{
				item.CachedGameObject.AddComponent<AutoReturnToPool<T>>().Init(this, item, time);
			}
			return item;
		}

		public void Return(T obj)
		{
			obj.CachedGameObject.SetActive(false);
			obj.CachedTransform.SetParent(_poolParent);
			_objects.Push(obj);	
		}
	}
}
