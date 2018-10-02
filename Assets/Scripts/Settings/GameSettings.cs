using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(fileName = "GameLevelsSettings",menuName = "Settings/Game")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField]
		private int _scoresForCity;
		
		[SerializeField]
		private int _playerProjectileSpeed;
	
		[SerializeField]
		private int _playerSpawnerCapacity;
		[SerializeField]
		private int [] _scoreMultiplier;

		public int[] ScoreMultiplier => _scoreMultiplier;
	

		public int PlayerProjectileSpeed => _playerProjectileSpeed;

		public int PlayerSpawnerCapacity => _playerSpawnerCapacity;



		public int ScoresForCity => _scoresForCity;

		
	}
}
