using System;
using FunnyBlox.Game;
using UnityEngine;

namespace FunnyBlox
{
  public class EventsHandler
  {
    public static event Action<Vector3, Vector3> OnCreateNewBall;
    public static void CreateNewBall(Vector3 position, Vector3 direction)
    {
      OnCreateNewBall?.Invoke(position, direction);
    }

    public static event Action<BallData> OnCreateNewBallOnConnectBalls;
    public static void CreateNewBallOnConnectBalls(BallData ballData)
    {
      OnCreateNewBallOnConnectBalls?.Invoke(ballData);
    }
    
    public static event Action<int> OnUpdateAmountBalls;
    public static void UpdateAmountBalls(int value)
    {
      OnUpdateAmountBalls?.Invoke(value);
    }
    
    
    public static event Action<int> OnUpdateScore;
    public static void UpdateScore(int value)
    {
      OnUpdateScore?.Invoke(value);
    }
    
    public static event Action OnResetWorld;
    public static void ResetWorld()
    {
      OnResetWorld?.Invoke();
    }
    
    public static event Action OnGameOver;
    public static void GameOver()
    {
      OnGameOver?.Invoke();
    }
    
  }
}