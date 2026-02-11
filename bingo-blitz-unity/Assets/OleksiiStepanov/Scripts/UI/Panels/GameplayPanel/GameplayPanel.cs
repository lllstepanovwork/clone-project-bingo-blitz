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
        [SerializeField] private Transform pauseButtonTransform;
        [SerializeField] private Transform rewardButtonTransform;
        [SerializeField] private GameObject combinationAnimation;
        
        private GameplayPanelLayout _currentLayout;
        private UIManager _uiManager;
        private SignalBus _signalBus;

        private bool _pause = false;
        
        [Inject]
        public void Construct(SignalBus signalBus, UIManager uiManager)
        {
            _signalBus = signalBus;
            _uiManager = uiManager;
        }
        
        private void OnEnable()
        {
            _signalBus.Subscribe<GameplayPanelLayout.CompletedSignal>(OnGameplayLayoutCompleted);
            _signalBus.Subscribe<BingoSequence.CompletedSignal>(OnBingoSequenceFinished);
        }
        
        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameplayPanelLayout.CompletedSignal>(OnGameplayLayoutCompleted);
            _signalBus.Unsubscribe<BingoSequence.CompletedSignal>(OnBingoSequenceFinished);
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
            _pause = false;
            
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
            pauseButtonTransform.gameObject.SetActive(true);
            rewardButtonTransform.gameObject.SetActive(true);

            backButtonTransform.DOShakeScale(0.2f, 0.5f);
            pauseButtonTransform.DOShakeScale(0.2f, 0.5f);
            rewardButtonTransform.DOShakeScale(0.2f, 0.5f);
            
            comboCounter.transform.DOShakeScale(0.2f, 0.5f);
            combinationAnimation.transform.DOShakeScale(0.2f, 0.5f);
        }

        public void OnBackButtonClicked()
        {
            backButtonTransform.gameObject.SetActive(false);
            pauseButtonTransform.gameObject.SetActive(false);
            rewardButtonTransform.gameObject.SetActive(false);
            
            _currentLayout.StopGame();
            _uiManager.OpenLevelPanel();
        }
        
        public void OnPauseButtonClicked()
        {
            _pause = !_pause;
                
            if (_pause)
            {
                messageText.ShowMessage(Constants.GAMEPLAY_MESSAGE_PAUSE, () =>
                {
                    _signalBus.Fire(new PauseSignal(_pause));
                });
            }
            else
            {
                messageText.ShowMessage(Constants.GAMEPLAY_MESSAGE_CONTINUE, () =>
                {
                    _signalBus.Fire(new PauseSignal(_pause));
                });   
            }
        }

        public void OnRewardButtonClicked()
        {
            _signalBus.Fire(new RewardSignal());
        }

        private void HideAllLayouts()
        {
            foreach (var layout in layouts)
            {
                layout.gameObject.SetActive(false);
            }
        }

        private void OnGameplayLayoutCompleted()
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
            pauseButtonTransform.gameObject.SetActive(false);
            rewardButtonTransform.gameObject.SetActive(false);
        }
    }    
}

