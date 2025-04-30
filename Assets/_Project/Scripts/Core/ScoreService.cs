using TMPro;
using UnityEngine;

namespace FunnyBlox.Game
{
  public class ScoreService : MonoBehaviour
  {
    [SerializeField] private TMP_Text labelScore;

    private int score;

    private void OnEnable()
    {
      EventsHandler.OnUpdateScore += OnUpdateScore;
      EventsHandler.OnResetWorld += OnResetWorld;
    }

    private void OnDisable()
    {
      EventsHandler.OnUpdateScore -= OnUpdateScore;
      EventsHandler.OnResetWorld -= OnResetWorld;
    }
    
    private void OnResetWorld()
    {
      score = 0;
      labelScore.text = score.ToString();
    }
    private void OnUpdateScore(int value)
    {
      score += value;
      labelScore.text = score.ToString();
    }
  }
}