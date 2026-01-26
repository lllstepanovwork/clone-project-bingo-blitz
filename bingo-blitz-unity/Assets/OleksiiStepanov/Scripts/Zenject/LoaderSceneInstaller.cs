using Zenject;

namespace BingoBlitzClone.Loading
{
    public class LoaderSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AppLoader>().AsSingle();
        }
    }
}
