using Common.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MenuState
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
        _scoresLabel.text = "Your score: \n" + score;
    }
        
    public void RestartGameButton()
    {
        SoundPlayer.Play("select");
        GameManager.StartNewGame();
    }
}
