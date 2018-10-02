using System;
using UnityEngine;

public static class GameScore
{
	private const string TOPSCORE_KEY = "TopScore";
	
	public static int TopScore
	{
		get { return PlayerPrefs.GetInt(TOPSCORE_KEY, 0); }
		private set
		{
			PlayerPrefs.SetInt(TOPSCORE_KEY, value);
		}
		
	}
	
	public static int Scores { get; private set; }

	public static int ScoreMultiplier { get; private set; } = 1;

	public static event Action<int> OnUpdateScore;

	static GameScore()
	{
		GameManager.UpdateLevelSettings += SetMultiplier;
	}

	private static void SetMultiplier(int level)
	{
		ScoreMultiplier = GameManager.Settings.ScoreMultiplier[Mathf.Min(GameManager.Settings.ScoreMultiplier.Length - 1, level)];
	}

	public static void AddScore(int score)
	{
		if (GameManager.CurrentState != GameState.Game)
			return;
		Scores += score * ScoreMultiplier;
		if (TopScore < Scores)
		{
			TopScore = Scores;
		}
		OnUpdateScore?.Invoke(Scores);
	}

	public static void Reset()
	{
		Scores = 0;
		ScoreMultiplier = 1;
	}
	
		
}
