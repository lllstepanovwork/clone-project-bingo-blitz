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
        }

        private void BindClasses()
        {
        }
    }
}
