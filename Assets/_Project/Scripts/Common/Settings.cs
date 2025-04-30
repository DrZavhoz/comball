using Sirenix.OdinInspector;
using UnityEngine;

namespace FunnyBlox
{
  [CreateAssetMenu(menuName = "Data/Create Settings", fileName = "Settings", order = 0)]
  public class Settings : ScriptableObject
  {
    [LabelText("Количество шаров для выстрела")]
    public int AmountBallsInCannon = 15;
    
    [LabelText("Максимальное значение нового шара")]
    public int BallValueMax = 3;
    
    [LabelText("Сила выстрела шара")]
    public float BallTurnForce = 20f;
    
    [LabelText("Цвета шара")]
    public Color[] BallColors;
  }
}