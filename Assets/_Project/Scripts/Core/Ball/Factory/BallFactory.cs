using FunnyBlox.Utils;
using UnityEngine;
using Zenject;

namespace FunnyBlox.Game
{
  public class BallFactory : IBallFactory
  {
    private Ball _ball;

    public void Load()
    {
      _ball = Resources.Load<Ball>("Gameplay/Ball");
    }

    public Ball CreateBall()
    {
      if(!_ball) Load();
      Ball ball = (Ball)PoolCollection.Spawn(_ball, Vector3.zero, Quaternion.identity).Component;
      return ball;
    }
  }
}