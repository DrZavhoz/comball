using System;
using UnityEngine;

namespace FunnyBlox.Game
{
  public interface IInputService
  {
    event Action<Vector3> OnMouseButtonDown;
    event Action<Vector3> OnMouseButton;
    event Action<Vector3> OnMouseButtonUp;
  }
}