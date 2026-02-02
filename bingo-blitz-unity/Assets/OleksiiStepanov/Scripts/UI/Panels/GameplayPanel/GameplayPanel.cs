using System;
using System.Collections.Generic;
using BingoBlitzClone.Game;
using BingoBlitzClone.Gameplay;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.UI
{
    public class GameplayPanel : UIPanel
    {
        [Header("Content")]
        [SerializeField] private GameplayPanelMessageText messageText;
        [SerializeField] private ComboCounter comboCounter;
        [SerializeField] private List<GameplayPanelLayout> layouts;
        [SerializeField] private Transform backButtonTransform;
        [SerializeField] private GameObject combinationAnimation;
        
        private GameplayPanelLayout _currentLayout;
        private UIManager _uiManager;

        [Inject]
        public void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        private void OnEnable()
        {
            GameplayPanelLayout.OnWin += OnWin;
            BingoSequence.OnSequenceFinished += OnBingoSequenceFinished;
        }
        
        private void OnDisable()
        {
            GameplayPanelLayout.OnWin -= OnWin;
            BingoSequence.OnSequenceFinished -= OnBingoSequenceFinished;
        }

        public void Init(int numberOfFields)
        {
            HideAllLayouts();

            _currentLayout = layouts[numberOfFields-1];
            _currentLayout.gameObject.SetActive(true);
            _currentLayout.Init();
            
            comboCounter.Init();
        }

        public void StartGame()
        {
            messageText.ShowMessage(Constants.GAMEPLAY_MESSAGE_GO, () =>
            {
                _currentLayout.StartGame();
            });
        }
        
        public override void OnUIPanelOpened()
        {
            comboCounter.gameObject.SetActive(true);
            combinationAnimation.SetActive(true);
            
            backButtonTransform.gameObject.SetActive(true);

            backButtonTransform.DOShakeScale(0.2f, 0.5f);
            comboCounter.transform.DOShakeScale(0.2f, 0.5f);
            combinationAnimation.transform.DOShakeScale(0.2f, 0.5f);
            
            _currentLayout.PlayShakeAnimation();
        }

        public void OnBackButtonClicked()
        {
            backButtonTransform.gameObject.SetActive(false);
            
            _currentLayout.StopGame();
            _uiManager.OpenLevelPanel();
        }

        private void HideAllLayouts()
        {
            foreach (var layout in layouts)
            {
                layout.gameObject.SetActive(false);
            }
        }

        private void OnWin()
        {
            OnGameOver();
            
            messageText.ShowMessage(Constants.GAMEPLAY_MESSAGE_YOU_WON, OnBackButtonClicked);
        }

        private void OnBingoSequenceFinished()
        {
            OnGameOver();
            
            messageText.ShowMessage(Constants.GAMEPLAY_MESSAGE_ROUND_OVER, OnBackButtonClicked);
        }

        private void OnGameOver()
        {
            comboCounter.gameObject.SetActive(false);
            
            combinationAnimation.SetActive(false);
            
            backButtonTransform.gameObject.SetActive(false);
        }
    }    
}

