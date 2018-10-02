using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(fileName = "SpawnEnemiesSettings",menuName = "Settings/Enemies")]
	public class EnemySettings : ScriptableObject 
	{
		[SerializeField]
		private GameObject _rocketPrefab;
    
		[SerializeField]
		private GameObject _planePrefab;
    
		[SerializeField]
		private GameObject _ufoPrefab;
		
		[SerializeField]
		private List<LevelSettings> _levels;
		
			
		[SerializeField]
		private float _pauseBetweenLevels;
		
		[SerializeField]
		private float _spawnTime;
	
		public List<LevelSettings> Levels => _levels;
	
    
		public float PauseBetweenLevels => _pauseBetweenLevels;
	
		public float SpawnTime => _spawnTime;

		public GameObject RocketPrefab => _rocketPrefab;

		public GameObject PlanePrefab => _planePrefab;

		public GameObject UfoPrefab => _ufoPrefab;
	}
}
