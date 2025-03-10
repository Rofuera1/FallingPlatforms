using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        BindSignals();

        Container.Bind<GameflowVisualManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<MapGenerator>().FromComponentInHierarchy().AsCached();
        Container.Bind<ColorChooser>().FromComponentInHierarchy().AsCached();
        Container.Bind<BlocksPainter>().FromComponentInHierarchy().AsCached();
        Container.Bind<ColorsContainer>().FromComponentInHierarchy().AsCached();
    }

    private void BindSignals()
    {
        Container.BindSignal<GameflowManager.GameflowEvent>();
    }
}
