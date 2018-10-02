using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityScore : MonoBehaviour
{

	[SerializeField] private Destroyable _city;
	
	[SerializeField]
	private TextMeshProUGUI _scoreLabel;

	[SerializeField]
	private float _showTime = 3f;
	
	private float _timer;
	
	private void Awake()
	{
		GameManager.OnLevelChange += ShowScore;
		_scoreLabel.enabled = false;
		enabled = false;
	}

	private void ShowScore(int level)
	{
		if (GameManager.CurrentState != GameState.Game || _city.IsDestroyed || level == 0)
		{
			return;
		}
		_scoreLabel.text = (GameManager.Settings.ScoresForCity * GameManager.Settings.ScoreMultiplier[level-1]).ToString();
		_timer = _showTime;
		_scoreLabel.enabled = true;
		enabled = true;
	}

	private void Update()
	{
		_timer -= Time.deltaTime;
		if (_timer < 0)
		{
			enabled = false;
			_scoreLabel.enabled = false;
			return;
		}

		_scoreLabel.rectTransform.anchoredPosition =
			Vector3.Lerp(Vector3.zero, 50 * Vector3.up, 1 - _timer / _showTime);
	}
}
