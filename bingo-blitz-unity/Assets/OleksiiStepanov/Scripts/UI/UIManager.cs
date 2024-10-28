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
            
            onComplete?.Invoke();
        }

        public void OpenLevelPanel()
        {
            _uIPanelOpener.ClosePanel(_currentUIPanel, () =>
            {
                _currentUIPanel = levelPanel;
                _uIPanelOpener.OpenPanel(levelPanel);
            });
        }

        public void OpenGameplayPanel(int layoutNumber)
        {
            _uIPanelOpener.ClosePanel(_currentUIPanel, () =>
            {
                gameplayPanel.Init(1);
                _uIPanelOpener.OpenPanel(gameplayPanel);
            });
        }
    }
}
