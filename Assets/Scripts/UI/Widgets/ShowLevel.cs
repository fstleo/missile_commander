using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowLevel : MonoBehaviour 
{
    [SerializeField]
    private TextMeshProUGUI _levelLabel;

    [SerializeField]
    private float _showTime = 3f;
	
    private float _timer;
	
    private void Awake()
    {
        GameManager.OnLevelChange += ShowLevelNumber;
        _levelLabel.enabled = false;
        enabled = false;
    }

    private void ShowLevelNumber(int level)
    {
        if (GameManager.CurrentState != GameState.Game)
        {
            return;
        }
        
        _levelLabel.text = "Level: " + (level + 1).ToString();
        _timer = _showTime;
        _levelLabel.enabled = true;
        enabled = true;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            enabled = false;
            _levelLabel.enabled = false;
            return;
        }
    }
}
