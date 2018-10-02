using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntitiesWatcher : MonoBehaviour
{

	private IDestroyable [] _playerDestroyableThings;	
	
	private void Awake()
	{
		_playerDestroyableThings = GetComponentsInChildren<IDestroyable>();
		GameManager.OnLevelChange += ProcessLevelChange; 
		foreach (var destroyable in _playerDestroyableThings)
		{
			destroyable.OnDestroy += CheckGameOver;
		}
	}

	private void ProcessLevelChange(int level)
	{
		if (GameManager.CurrentState != GameState.Game)
		{
			return;
		}

		if (level == 0)
		{
			foreach (var city in _playerDestroyableThings)
			{
				city.Reset();
			}
		}
		else
		{
			foreach (var city in _playerDestroyableThings)
			{
				if (!city.IsDestroyed)
				{
					GameScore.AddScore(GameManager.Settings.ScoresForCity);
				}
			}
		}
	}

	private void CheckGameOver(Owner obj)
	{		
		if (_playerDestroyableThings.Any(destroyable => !destroyable.IsDestroyed))
		{
			return;
		}

		if (GameManager.CurrentState != GameState.Game)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		GameManager.GameOver();
	}
}
