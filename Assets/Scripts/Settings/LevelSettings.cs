using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerType
{
    Explosing,
    Splitting,
    MiddlePlane,
    MiddleUFO,
    LowPlane,
    LowUFO
}
[Serializable]
public class SpawnSetting
{

    [SerializeField] 
    private SpawnerType _spawnerType;
   
    [SerializeField] 
    private int _count;

    [SerializeField] private int _projectileSpeed;


    public SpawnerType SpawnerType => _spawnerType;

    public int Count => _count;

    public int ProjectileSpeed => _projectileSpeed;
    
}

[Serializable]
public class LevelSettings 
{
    
    [SerializeField]
    private List<SpawnSetting> _spawnSettings;

    public List<SpawnSetting> SpawnSettings => _spawnSettings;
}
