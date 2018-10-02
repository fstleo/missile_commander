using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoresUI : MenuState
{    
    [SerializeField]
    private TextMeshProUGUI _scoresLabel;

    protected override void Awake()
    {
        base.Awake();        
        GameScore.OnUpdateScore += UpdateScoreLabel;        
        UpdateScoreLabel(0);
    }


    private void UpdateScoreLabel(int score)
    {
        _scoresLabel.text = score.ToString();
    }
}
