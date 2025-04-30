using UnityEngine;

namespace FunnyBlox
{
  public class Bootstrap : MonoBehaviour
  {
    private void Start()
    {
#if  UNITY_ANDROID || UNITY_IOS
      Application.targetFrameRate = 60;
#endif
      EventsHandler.ResetWorld();
    }
  }
}