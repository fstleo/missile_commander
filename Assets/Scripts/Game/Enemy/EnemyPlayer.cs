using System.Diagnostics.Contracts;
using System.Linq;
using Game;
using Game.Spawn;
using Settings;
using UnityEngine;


public class EnemyPlayer : MonoBehaviour
{
    
    [SerializeField]
    private Transform[] _targets;
    
    private float _timer;

    [HideInInspector]
    public bool CanSpawn;
    private ISpawner[] _spawners;
    
    private EnemySettings _settings;

    private void Awake()
    {
        _settings = Resources.Load<EnemySettings>("Settings/Enemies");
    }

    private void Start()
    {
        InitializeSpawners();
        GameManager.OnLevelChange += OnLevelChange;
        GameManager.OnStateChange += CheckState;
                
        _timer = _settings.PauseBetweenLevels;
        GameManager.CurrentLevel = 0;
    }

    private void CheckState(GameState state)
    {
        if (state == GameState.GameOver)
        {
            CanSpawn = false;
        }
    }

    private void OnLevelChange(int level)
    {
        if (GameManager.CurrentState == GameState.GameOver)
        {
            return;
        }
        for (int i = 0; i < _spawners.Length; i++)
        {
            _spawners[i].Set(0,0);
        }
        
        foreach (var setting in _settings.Levels[Mathf.Min(_settings.Levels.Count -1, level)].SpawnSettings)
        {
            _spawners[(int)setting.SpawnerType].Set(setting.Count, setting.ProjectileSpeed);
        }

        CanSpawn = true;
        _timer = _settings.PauseBetweenLevels;
    }


    private void InitializeSpawners()
    {
        var targetsPositions = _targets.Select(t => t.position).ToArray();
        SpawnEnemyCommandsFactory commandsFactory =
            new SpawnEnemyCommandsFactory(targetsPositions, _settings.RocketPrefab, _settings.PlanePrefab, _settings.UfoPrefab);
        
        
        var spawners = GetComponentsInChildren<EnemySpawnerHandler>();
        _spawners = new ISpawner[spawners.Length];
        
        
        foreach (var spawner in spawners)
        {
            spawner.Init(commandsFactory.Create(spawner.Type));
            _spawners[(int) spawner.Type] = spawner;            
        }
    }


    private void Update ()
    {
        if (!CanSpawn)
        {
            return;
        }
        _timer -= Time.deltaTime;
        if (_timer > 0)
        {
            return;
        }        
        SpawnSomething();

    }

    private void SpawnSomething()
    {
        int fullCapacity = 0;
        
        for(int i = 0; i < _spawners.Length;i++)
        {
            fullCapacity += _spawners[i].Capacity;
        }
    
        if (fullCapacity == 0)
        {
            _timer = _settings.PauseBetweenLevels;            
            CanSpawn = false;
            return;
        }

        var _randomSpawnValue = Random.value;
        int offset = 0;
        
        foreach (var spawner in _spawners)
        {
            if (1f * (spawner.Capacity + offset) / fullCapacity > _randomSpawnValue && spawner.Capacity > 0)
            {
                _timer = _settings.SpawnTime;
                spawner.Spawn(_targets[Random.Range(0, _targets.Length)].position);
                return;
            }

            offset += spawner.Capacity;
        }


    }
}


