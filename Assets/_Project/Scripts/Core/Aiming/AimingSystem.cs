using System;
using UnityEngine;
using Zenject;

namespace FunnyBlox.Game
{
  public class AimingSystem : MonoBehaviour
  {
    [SerializeField] private Transform rootAimingTransform;
    private Transform _leftAimingTransform;
    private Transform _rightAimingTransform;

    [SerializeField] private Transform targetAimingTransform;

    [SerializeField] private Transform aimingLineTransform;
    [SerializeField] private Material aimingDotsMaterial;

    private Vector3 _startingAimPosition;
    private Vector3 _targetAimPosition;
    private Vector3 _hitPosition;

    private Vector3 _rayDirection;
    private RaycastHit _raycastHit;
    private int _gameLayer;

    private float _aimingLenght;


    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
      _inputService = inputService;
      _inputService.OnMouseButtonDown += OnMouseButtonDown;
      _inputService.OnMouseButton += OnMouseButton;
      _inputService.OnMouseButtonUp += OnMouseButtonUp;
    }

    private void OnEnable()
    {
      
    }

    private void OnDestroy()
    {
      _inputService.OnMouseButtonDown -= OnMouseButtonDown;
      _inputService.OnMouseButton -= OnMouseButton;
      _inputService.OnMouseButtonUp -= OnMouseButtonUp;
    }

    void Start()
    {
      ShowAimingObjects(false);
      _gameLayer = 1 << LayerMask.NameToLayer("Balls");
      _leftAimingTransform = rootAimingTransform.GetChild(0);
      _rightAimingTransform = rootAimingTransform.GetChild(1);

      CommonData.GameState = EGameState.GamePlay;
    }

    private void OnMouseButtonDown(Vector3 position)
    {
      if (CommonData.GameState != EGameState.GamePlay) return;

      CommonData.GameState = EGameState.GameAiming;

      ShowAimingObjects(true);

      _startingAimPosition = position;
      _startingAimPosition.y = 0f;
    }

    private void OnMouseButton(Vector3 position)
    {
      if (CommonData.GameState == EGameState.GameAiming)
      {
        _targetAimPosition = position; // - _startingAimPosition;
        CorrectAimPosition();
        CalculateAimingLine();
      }
    }

    private void OnMouseButtonUp(Vector3 position)
    {
      if (CommonData.GameState != EGameState.GameAiming) return;

      CommonData.GameState = EGameState.GamePlay;

      ShowAimingObjects(false);

      _targetAimPosition = position; // - _startingAimPosition;
      CorrectAimPosition();
      EventsHandler.CreateNewBall(rootAimingTransform.position, _targetAimPosition - rootAimingTransform.position);
    }

    private void CalculateAimingLine()
    {
      _rayDirection = _targetAimPosition - rootAimingTransform.position;

      rootAimingTransform.rotation = Quaternion.LookRotation(_rayDirection);

      _aimingLenght = GetDistance(rootAimingTransform) - 0.5f;
      _aimingLenght = Mathf.Min(_aimingLenght, GetDistance(_leftAimingTransform));
      _aimingLenght = Mathf.Min(_aimingLenght, GetDistance(_rightAimingTransform));

      targetAimingTransform.position = rootAimingTransform.position;
      targetAimingTransform.forward = rootAimingTransform.forward;
      targetAimingTransform.Translate(0f, 0f, _aimingLenght);

      aimingLineTransform.position = rootAimingTransform.position;
      aimingLineTransform.forward = rootAimingTransform.forward;
      aimingLineTransform.localScale = new Vector3(1f, 1f, _aimingLenght);

      aimingDotsMaterial.mainTextureScale = new Vector2(1f, _aimingLenght * 2f);
    }

    private float GetDistance(Transform point)
    {
      if (Physics.Raycast(point.position, point.forward, out _raycastHit, 20f, _gameLayer))
      {
        DrawDebugLine(point.position, _raycastHit.point);
        return (_raycastHit.point - point.position).magnitude;
      }

      return 100f;
    }

    private void ShowAimingObjects(bool state)
    {
      aimingLineTransform.gameObject.SetActive(state);
      targetAimingTransform.gameObject.SetActive(state);
    }

    private void CorrectAimPosition()
    {
      _targetAimPosition.y = 0f;
      if (_targetAimPosition.z < 1f)
        _targetAimPosition.z = 1f;
      if (_targetAimPosition.x < -3f)
        _targetAimPosition.x = -3f;
      else if (_targetAimPosition.x > 3f)
        _targetAimPosition.x = 3f;
    }

    //////// DEBUG ////////
    private void DrawDebugLine(Vector3 from, Vector3 to)
    {
      Debug.DrawLine(from, to, Color.red);
    }
  }
}