using System;
using UnityEngine;
using Util.ObjectPool;
using Utils;

public class PoolItem : MonoBehaviour
{
    public event Action<GameObject> OnReturnToPool;
    
    private IObjectPool _pool;
    public GameObject GameObject { get; private set; }
    private Transform _transform;

    public virtual void Init(IObjectPool<GameObject> pool)
    {
        GameObject = gameObject;
        _transform = transform;
        _pool = pool;
    }

    public virtual void Return()
    {
        OnReturnToPool?.Invoke(GameObject);
        _pool.Return(GameObject);
    }

    public virtual void ResetItem()
    {
        GameObject.SetActive(true);
        _transform.SetParent(null);
    }
}
