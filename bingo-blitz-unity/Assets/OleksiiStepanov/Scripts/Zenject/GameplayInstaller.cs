using BingoBlitzClone.UI;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            DeclareSignals();
            BindClasses();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<LayoutSelectedSignal>();
        }

        private void BindClasses()
        {
            Container.BindInterfacesAndSelfTo<BingoLogic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BingoSequence>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BingoCombinations>().AsSingle().NonLazy();
        }
    }
}
