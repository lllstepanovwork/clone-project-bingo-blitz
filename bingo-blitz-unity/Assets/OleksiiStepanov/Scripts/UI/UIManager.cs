using System;
using OleksiiStepanov.Utils;
using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        [SerializeField] private GameplayPanel gameplayPanel;
        [SerializeField] private LevelPanel levelPanel;
        [SerializeField] private TransitionPanel transitionPanel;

        private readonly UIPanelOpener _uIPanelOpener = new UIPanelOpener();
        
        private UIPanel _currentUIPanel;

        public void Init(Action onComplete = null)
        {
            _currentUIPanel = levelPanel;
            _uIPanelOpener.OpenPanel(levelPanel);
            levelPanel.Init();
            
            onComplete?.Invoke();
        }

        public void OpenLevelPanel()
        {
            OpenPanelWithTransition(levelPanel);
        }

        public void OpenGameplayPanel(int layoutNumber)
        {
            gameplayPanel.Init(layoutNumber);
            OpenPanelWithTransition(gameplayPanel, null, gameplayPanel.StartGame);
        }
        
        private void OpenPanelWithTransition(UIPanel uiPanel, Action onTransitionMiddlePoint = null, Action onComplete = null)
        {
            _uIPanelOpener.OpenPanel(transitionPanel, () =>
            {
                transitionPanel.Init(() =>
                {
                    _uIPanelOpener.ClosePanel(_currentUIPanel, () =>
                    {
                        _currentUIPanel = uiPanel;
                    });
                }, () =>
                {
                    _uIPanelOpener.OpenPanel(uiPanel);
                    onTransitionMiddlePoint?.Invoke();
                }, onComplete);
            });
        }
    }
}
