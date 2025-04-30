using System;
using UnityEngine;

namespace FunnyBlox.Game
{
  public class InputService:MonoBehaviour, IInputService
  {
    public event Action<Vector3> OnMouseButtonDown;
    public event Action<Vector3> OnMouseButton;
    public event Action<Vector3> OnMouseButtonUp;
    
    private Camera _camera;
    
    private void Start()
    {
      _camera = Camera.main;
    }
    
    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        OnMouseButtonDown?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
      }
      if (Input.GetMouseButton(0))
      {
        OnMouseButton?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
      }
      if (Input.GetMouseButtonUp(0))
      {
        OnMouseButtonUp?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
      }
    }
  }
}