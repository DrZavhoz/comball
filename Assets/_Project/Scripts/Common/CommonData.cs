namespace FunnyBlox
{
  public enum EGameState
  {
    GamePlay=2,
    GameAiming = 3,
    GameWait = 4,
    GameOver=5,
    
  }

  public class CommonData
  {
    public static EGameState GameState ;

    public static int AmountBallsInCannon;
  }
}