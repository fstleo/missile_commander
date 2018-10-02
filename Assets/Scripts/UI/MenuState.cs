using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : MonoBehaviour
{
	[SerializeField] private GameState _state;
	
	protected virtual void Awake()
	{
		GameManager.OnStateChange += GameStateChange;
		GameStateChange(GameManager.CurrentState);
	}

	protected void GameStateChange(GameState state)
	{
		gameObject.SetActive(state == _state);
	}
}
