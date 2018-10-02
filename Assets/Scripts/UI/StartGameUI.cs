using System.Collections;
using System.Collections.Generic;
using Common.Sound;
using TMPro;
using UnityEngine;

public class StartGameUI : MenuState
{
    [SerializeField] private TextMeshProUGUI _topScoreLabel;
    
    protected override void Awake()
    {
        base.Awake();
        _topScoreLabel.text = "Top score: " + GameScore.TopScore;
    }
    
    public void StartGameButton()
    {
        SoundPlayer.Play("select");
        GameManager.StartNewGame();
    }
    
}
