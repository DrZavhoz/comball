using FunnyBlox.Game;
using Zenject;

namespace FunnyBlox
{
  public class BootstrapInstaller : MonoInstaller
  {
    public InputService inputService;

    public override void InstallBindings()
    {
      BindInputSecvice();
      BindGameplayFactories();
    }
    
    private void BindInputSecvice()
    {
      Container
        .Bind<IInputService>()
        .To<InputService>()
        .FromComponentInNewPrefab(inputService)
        .AsSingle();
    }

    private void BindGameplayFactories()
    {
      Container
        .Bind<IBallFactory>()
        .To<BallFactory>()
        .AsSingle();
    }
  }
}