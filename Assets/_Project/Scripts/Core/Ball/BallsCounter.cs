using System.Collections;
using TMPro;
using UnityEngine;

namespace FunnyBlox.Game
{
  public class BallsCounter : MonoBehaviour
  {
    [SerializeField] private Settings settings;
    [SerializeField] private TMP_Text labelCounterBalls;

    private void OnEnable()
    {
      EventsHandler.OnResetWorld += SetupCounter;
      EventsHandler.OnUpdateAmountBalls += UpdateAmountBalls;
    }

    private void OnDisable()
    {
      EventsHandler.OnResetWorld -= SetupCounter;
      EventsHandler.OnUpdateAmountBalls -= UpdateAmountBalls;
    }

    private void SetupCounter()
    {
      CommonData.AmountBallsInCannon = settings.AmountBallsInCannon;
    }

    private void UpdateAmountBalls(int value)
    {
      if(CommonData.GameState == EGameState.GameOver) return;
      
      CommonData.AmountBallsInCannon += value;

      if (CommonData.AmountBallsInCannon <= 0)
      {
        StartCoroutine(GameOverRoutine());
        CommonData.GameState = EGameState.GameOver;
      }
      else
      {
        CommonData.GameState = EGameState.GamePlay;

        StopAllCoroutines();
        if (CommonData.AmountBallsInCannon > settings.AmountBallsInCannon)
          CommonData.AmountBallsInCannon = settings.AmountBallsInCannon;
      }

      labelCounterBalls.text = CommonData.AmountBallsInCannon.ToString();
    }

    private IEnumerator GameOverRoutine()
    {
      yield return new WaitForSeconds(3f);

      EventsHandler.GameOver();
    }
  }
}