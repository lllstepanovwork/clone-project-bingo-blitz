using System;
using BingoBlitzClone.Utils;
using MyNamespace;
using UnityEngine;

namespace BingoBlitzClone.UI
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        [Header("Content")]
        [SerializeField] private GameplayPanel gameplayPanel;
        [SerializeField] private LevelPanel levelPanel;
        [SerializeField] private LoadingPanel loadingPanel;
        [SerializeField] private TransitionPanel transitionPanel;
        [SerializeField] private AboutPanel aboutPanel;
        
        private readonly UIPanelOpener _uIPanelOpener = new UIPanelOpener();
        
        private UIPanel _currentUIPanel;

        public void Init(Action onComplete = null)
        {
            _currentUIPanel = loadingPanel;
            _uIPanelOpener.OpenPanel(loadingPanel);
            
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

        public void OpenAboutPanel(Action onContinueButtonClicked = null)
        {
            aboutPanel.Init(() =>
            {
                onContinueButtonClicked?.Invoke();
                _uIPanelOpener.ClosePanel(aboutPanel);  
            });
            
            _uIPanelOpener.OpenPanel(aboutPanel);
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
                }, () =>
                {
                    _uIPanelOpener.ClosePanel(transitionPanel);
                    onComplete?.Invoke();    
                });
            });
        }
    }
}
