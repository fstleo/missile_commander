using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAircraftSpawnerHandler : EnemySpawnerHandler 
{
    public override void Spawn(Vector3 target)
    {
        Capacity--;
        bool reverse = Random.value > 0.5f;
        _command.Spawn(
            reverse ? _leftBorder.position : _rightBorder.position, 
            reverse ? _rightBorder.position : _leftBorder.position, 
            _currentLevel
        );
    }
}
