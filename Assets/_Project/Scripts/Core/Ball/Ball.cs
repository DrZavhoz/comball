using System;
using FunnyBlox.Utils;
using UnityEngine;

namespace FunnyBlox.Game
{
  [System.Serializable]
  public class BallData
  {
    public int Value;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 LinearVelocity;
    public Vector3 AngularVelocity;
    public int Layer;
  }

  public class Ball : MonoBehaviour
  {
    [SerializeField] private Settings settings;

    public bool IsActive => _isActive;
    private bool _isActive;

    public int Value => _value;
    private int _value;

    private Rigidbody _rigidbody;
    private Material _material;

    private Vector3 _previousLinearVelocity;
    private Vector3 _previousAngularVelocity;

    public float Speed => _previousLinearVelocity.magnitude;

    private void Initialize()
    {
      if (_rigidbody) return;

      EventsHandler.OnResetWorld += Despawn;

      _rigidbody = GetComponent<Rigidbody>();

      _material = GetComponent<Renderer>().material;
    }

    private void OnDisable()
    {
      EventsHandler.OnResetWorld -= Despawn;
    }

    public void SpawnFromCannon(Vector3 position, int value, Vector3 direction)
    {
      Initialize();

      _isActive = true;
      gameObject.layer = 9;
      _value = value;
      transform.position = position;

      _rigidbody.isKinematic = false;
      _rigidbody.AddForce(direction.normalized * settings.BallTurnForce);

      Debug.Log(direction.normalized);

      _material.color = settings.BallColors[_value];
    }

    public void SpawnAfterConnect(BallData ballData)
    {
      Initialize();

      _isActive = true;
      _value = ballData.Value;
      gameObject.layer = ballData.Layer;
      transform.position = ballData.Position;
      transform.rotation = ballData.Rotation;

      _rigidbody.isKinematic = false;
      _rigidbody.linearVelocity = ballData.LinearVelocity;
      _rigidbody.angularVelocity = ballData.AngularVelocity;

      _material.color = settings.BallColors[_value];
    }

    public void Despawn()
    {
      if (!_isActive) return;
      _isActive = false;
      _rigidbody.linearVelocity = Vector3.zero;
      _rigidbody.angularVelocity = Vector3.zero;
      _rigidbody.isKinematic = true;

      PoolCollection.Unspawn(transform);
    }

    private void FixedUpdate()
    {
      _previousLinearVelocity = _rigidbody.linearVelocity;
      _previousAngularVelocity = _rigidbody.angularVelocity;
    }

    private void OnCollisionEnter(Collision other)
    {
      if (other.transform.CompareTag("Ball"))
      {
        if (!_isActive) return;

        if (other.transform.TryGetComponent(out Ball ball))
        {
          if (ball.IsActive && _value == ball.Value)
          {
            if (Speed > ball.Speed)
              CreateNewBall();
            else
              ball.CreateNewBall();

            EventsHandler.UpdateScore(_value + 1);

            ball.Despawn();
            Despawn();

            //TODO add feel effect on connect
          }
        }
        else
        {
          //TODO add sound effect on touch other ball
        }
      }
      else if (other.transform.CompareTag("Wall"))
      {
        //TODO add sound effect on touch wall
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("CannonZone"))
      {
        gameObject.layer = 6;
      }
    }

    public void CreateNewBall()
    {
      EventsHandler.CreateNewBallOnConnectBalls(
        new BallData
        {
          Value = _value + 1,
          Position = transform.position,
          Rotation = transform.rotation,
          LinearVelocity = _previousLinearVelocity,
          AngularVelocity = _previousAngularVelocity,
          Layer = gameObject.layer
        }
      );
    }
  }
}