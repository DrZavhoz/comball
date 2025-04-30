using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace FunnyBlox.Game
{
  public class BallsService : MonoBehaviour
  {
    [LabelText("Настройки")] [SerializeField]
    private Settings settings;

    [Space] [LabelText("Превью следующего шара")] [SerializeField]
    private Transform previewBallTransform;

    private Material _previewBallMaterial;

    private int _ballValue;

    private IBallFactory _ballFactory;

    [Inject]
    public void Construct(IBallFactory ballFactory)
    {
      _ballFactory = ballFactory;
    }

    private void OnEnable()
    {
      EventsHandler.OnResetWorld += OnResetWorld;
      EventsHandler.OnCreateNewBall += OnCreateNewBall;
      EventsHandler.OnCreateNewBallOnConnectBalls += OnCreateNewOnConnectBalls;
    }

    private void OnDisable()
    {
      EventsHandler.OnResetWorld -= OnResetWorld;
      EventsHandler.OnCreateNewBall -= OnCreateNewBall;
      EventsHandler.OnCreateNewBallOnConnectBalls -= OnCreateNewOnConnectBalls;
    }

    private void Start()
    {
      _previewBallMaterial = previewBallTransform.GetComponent<Renderer>().material;
      BallPreview();
    }

    private void OnResetWorld()
    {
      CommonData.GameState = EGameState.GamePlay;
    }
    private void OnCreateNewBall(Vector3 position, Vector3 direction)
    {
      Ball ball = _ballFactory.CreateBall();
      ball.SpawnFromCannon(position, _ballValue, direction);

      EventsHandler.UpdateAmountBalls(-1);
      
      BallPreview();
    }

    private void OnCreateNewOnConnectBalls(BallData ballData)
    {
      Ball ball = _ballFactory.CreateBall();
      ball.SpawnAfterConnect(ballData);
      
      EventsHandler.UpdateAmountBalls(1);
    }

    private void BallPreview()
    {
      _ballValue = Random.Range(0, settings.BallValueMax);
      _previewBallMaterial.color = settings.BallColors[_ballValue];
      previewBallTransform.position = new Vector3(0f, 0f, -2f);
      previewBallTransform.DOMoveZ(0, 0.3f);
    }
  }
}