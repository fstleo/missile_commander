using System;
using System.Collections;
using System.Collections.Generic;
using Game.Spawn;
using UnityEngine;

public interface ISpawner
{
    event Action<int> OnCapacityChange;
    
    int Capacity { get; }

    void Init(ISpawnCommand command);
    void Set(int capacity, int level);
    void Spawn(Vector3 target);
}
