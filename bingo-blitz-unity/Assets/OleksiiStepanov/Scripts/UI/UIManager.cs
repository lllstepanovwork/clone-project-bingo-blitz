using System;
using OleksiiStepanov.Utils;
using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class UIManager : SingletonBehaviour<UIManager>
    {
        [SerializeField] private GameplayPanel gameplayPanel;
        [SerializeField] private LevelPanel levelPanel;

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
            levelPanel.Init();
            
            _uIPanelOpener.ClosePanel(_currentUIPanel, () =>
            {
                _currentUIPanel = levelPanel;
                _uIPanelOpener.OpenPanel(levelPanel);
            });
        }

        public void OpenGameplayPanel(int layoutNumber)
        {
            gameplayPanel.Init(layoutNumber);
            
            _uIPanelOpener.ClosePanel(_currentUIPanel, () =>
            {
                _currentUIPanel = gameplayPanel;
                
                _uIPanelOpener.OpenPanel(gameplayPanel, () =>
                {
                    gameplayPanel.StartGame();
                });
            });
        }
    }
}
