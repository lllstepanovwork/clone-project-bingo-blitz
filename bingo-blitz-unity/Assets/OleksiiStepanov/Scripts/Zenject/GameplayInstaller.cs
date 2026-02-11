using BingoBlitzClone.UI;
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
            
            Container.DeclareSignal<BingoSequence.NewNumberSignal>();
            Container.DeclareSignal<BingoSequence.CompletedSignal>();
            
            Container.DeclareSignal<GameplayPanelLayout.CompletedSignal>();
            Container.DeclareSignal<PauseSignal>();
            Container.DeclareSignal<RewardSignal>();
            
            Container.DeclareSignal<BingoField.CompletedSignal>();
            Container.DeclareSignal<BingoLogic.NumberMatchSignal>();
            Container.DeclareSignal<BingoFieldElement.CompleteFieldButtonClickedSignal>();
        }

        private void BindClasses()
        {
            Container.BindInterfacesAndSelfTo<BingoLogic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BingoSequence>().AsSingle().NonLazy();
        }
    }
    
    public class PauseSignal
    {
        public bool Paused { get; private set; }
            
        public  PauseSignal(bool paused)
        {
            Paused = paused;
        }
    }
}
