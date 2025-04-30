using UnityEngine;

namespace FunnyBlox
{
  public class Bootstrap : MonoBehaviour
  {
    private void Start()
    {
      EventsHandler.ResetWorld();
    }
  }
}