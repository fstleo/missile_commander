using System;
using Settings;
using UnityEditor;
using UnityEngine;

public static class GameManager
{
    public static event Action<int> OnLevelChange;
    public static event Action<int> UpdateLevelSettings;
    public static event Action<GameState> OnStateChange;

    private static readonly GameSettings _settings;
    
    private static int _currentLevel = 0;
    private static GameState _currentState;
    
    public static GameState CurrentState
    {
        get { return _currentState; }
        private set
        {
            _currentState = value; 
            OnStateChange?.Invoke(_currentState);
        }
    }

    public static int CurrentLevel
    {
        get { return _currentLevel; }
        set
        {
            _currentLevel = value;
            OnLevelChange?.Invoke(_currentLevel);
            UpdateLevelSettings?.Invoke(_currentLevel);
        }
    }

    public static GameSettings Settings => _settings;
    
    static GameManager()
    {
        _settings = Resources.Load<GameSettings>("Settings/GameSettings");
    }

    public static void StartNewGame()
    {
        GameScore.Reset();
        CurrentState = GameState.Game;
        CurrentLevel = 0;
    }

    public static void LevelUp()
    {
        CurrentLevel++;        
    }

    public static void GameOver()
    {
        CurrentState = GameState.GameOver;
    }
    

}
