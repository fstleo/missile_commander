using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitiesWatcher : MonoBehaviour
{
    public bool IsSomethingInside => AliveEntitiesList.Count > 0;

    [HideInInspector]
    public HashSet<GameObject> AliveEntitiesList = new HashSet<GameObject>();

    private void Awake()
    {
        GameManager.OnStateChange += ClearField;
    }

    private void ClearField(GameState state)
    {
        var _array = AliveEntitiesList.ToArray();
        for (int i = 0; i < _array.Length; i++)
        {
            _array[i].GetComponent<IDestroyable>().Destroy(Owner.Enemy);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AliveEntitiesList.Add(other.gameObject);
        other.GetComponent<PoolItem>().OnReturnToPool += OnReturnToPool;
    }

    private void OnReturnToPool(GameObject go)
    {
        AliveEntitiesList.Remove(go);
    }
}
