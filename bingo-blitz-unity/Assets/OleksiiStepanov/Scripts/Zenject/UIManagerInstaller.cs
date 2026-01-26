using UnityEngine;
using Zenject;

namespace BingoBlitzClone.UI
{
    public class UIManagerInstaller : MonoInstaller
    {
        [SerializeField] private UIManager uiManager;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>()
                .FromInstance(uiManager)
                .AsSingle();
        }
    }
}