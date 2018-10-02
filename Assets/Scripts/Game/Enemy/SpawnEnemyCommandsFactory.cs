using Game.Spawn;
using Game.Spawn.Commands;
using UnityEngine;
using Util.ObjectPool;

namespace Game
{
    public class SpawnEnemyCommandsFactory 
    {   
        private readonly ISpawnCommand _spawnRocketCommand;
        private readonly ISpawnCommand _spawnSplittingCommand;
        private readonly ISpawnCommand _spawnPlaneCommand;
        private readonly ISpawnCommand _spawnUfoCommand;

        public SpawnEnemyCommandsFactory(Vector3[] targets, GameObject rocketPrefab, GameObject planePrefab, GameObject ufoPrefab)
        {
            var rocketPool = new GameObjectPool(rocketPrefab, 32);
            var planePool =  new GameObjectPool(planePrefab, 4);
            var ufoPool =  new GameObjectPool(ufoPrefab, 4);

            _spawnRocketCommand = new SpawnEntityCommand(rocketPool, Owner.Enemy);
            _spawnSplittingCommand = new SpawnSplittingEntityCommand(rocketPool, targets, Owner.Enemy, _spawnRocketCommand);
            _spawnPlaneCommand = new SpawnSpawningEntityCommand(planePool, targets, Owner.Enemy, _spawnRocketCommand);
            _spawnUfoCommand =  new SpawnSpawningEntityCommand(ufoPool, targets, Owner.Enemy, _spawnSplittingCommand);
            
        }

        public ISpawnCommand Create(SpawnerType spawnerType)
        {                    
            switch (spawnerType)
            {
                case SpawnerType.Explosing:
                    return _spawnRocketCommand;
                break;
                    
                case SpawnerType.MiddlePlane:        
                case SpawnerType.LowPlane:
                    return _spawnPlaneCommand;
                break;
                case SpawnerType.MiddleUFO:
                case SpawnerType.LowUFO:
                    return _spawnUfoCommand;
                break;
                case SpawnerType.Splitting:
                    return _spawnSplittingCommand;
                break;
            }
            Debug.LogError("Unknown spawner type");
            return null;        
        }
    }
}
