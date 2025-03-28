using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindSignals();

        Container.Bind<GameflowManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameflowVisualManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MapManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MapGenerator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ColorChooser>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BlocksPainter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ColorsContainer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerTransform>().FromComponentInHierarchy().AsSingle();

        Container.Bind<LightsOperator>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Spotlight>().FromComponentInHierarchy().AsSingle();

        Container.Bind<UI>().FromComponentInHierarchy().AsSingle();
    }

    private void BindSignals()
    {
        Container.DeclareSignal<GameflowManager.GameflowEvent>().OptionalSubscriber();
    }
}
