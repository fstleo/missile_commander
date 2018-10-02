using System;
using Game.Spawn;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerHandler : MonoBehaviour, ISpawner
{
	[SerializeField] protected Transform _leftBorder;
	[SerializeField] protected Transform _rightBorder;

	[SerializeField]
	protected SpawnerType _type;

	public SpawnerType Type => _type;

	protected ISpawnCommand _command;

	private Vector3[] _targets;

	public event Action<int> OnCapacityChange;

	public int Capacity
	{
		get { return _capacity; }
		protected set
		{
			_capacity = value;
			OnCapacityChange?.Invoke(_capacity);
		}
	}

	protected int _currentLevel;
	private int _capacity;

	public void Init(ISpawnCommand command)
	{
		_command = command;		
	}

	public void Set(int capacity, int level)
	{
		Capacity = capacity;
		_currentLevel = level;
	}

	public virtual void Spawn(Vector3 target)
	{
		Capacity--;
		_command.Spawn(
			Vector3.Lerp(_leftBorder.position, _rightBorder.position, Random.value),
			target, 
			_currentLevel
		);
	}
}