using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

	[SerializeField] private Camera _camera;

	[SerializeField] private Player _player;

	private void Awake()
	{		
		GameManager.OnStateChange += ListenToTheGameState;
		enabled = false;
	}

	private void ListenToTheGameState(GameState state)
	{
		enabled = state == GameState.Game;
	}
	
	private void Update () 
	{

		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Input.mousePosition;
			mousePosition = _camera.ScreenToWorldPoint(new Vector2(mousePosition.x, Mathf.Clamp(mousePosition.y, Screen.height/4f, Screen.height)));
			_player.SpawnProjectile(mousePosition);
		}
	}
}
