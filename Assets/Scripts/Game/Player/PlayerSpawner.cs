using System;
using System.Collections;
using System.Collections.Generic;
using Game.Spawn;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour , ISpawner
{
    public event Action<int> OnCapacityChange;

    public int Capacity
    {
        get { return _capacity; }
        private set
        {            
            _capacity = value;
            OnCapacityChange?.Invoke(_capacity);
        }
    }

    private ISpawnCommand _command;
    private IDestroyable _destroyable;

    private int _level;
    private int _capacity;

    public void Init(ISpawnCommand command)
    {
         _destroyable = GetComponent<IDestroyable>();
        _command = command;        
        _destroyable.OnDestroy += DestroySpawner;
    }

    private void DestroySpawner(Owner obj)
    {
        Set(0,0);
    }

    public void Set(int capacity, int level)
    {
        if (capacity > 0)
        {
            _destroyable.Reset();
        }

        Capacity = capacity;
        _level = level;
    }

    public void Spawn(Vector3 target)
    {
        Capacity--;
        _command.Spawn(transform.position, target, _level);
    }
}
