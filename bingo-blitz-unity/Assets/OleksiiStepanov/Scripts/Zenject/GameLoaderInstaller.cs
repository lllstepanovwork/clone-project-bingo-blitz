using Zenject;

namespace BingoBlitzClone.Loading
{
    public class GameLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingStepFactory>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameLoader>()
                .AsSingle()
                .NonLazy();
        }
    }
}
