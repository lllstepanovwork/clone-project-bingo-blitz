using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using OleksiiStepanov.Game;
using OleksiiStepanov.Gameplay;

namespace OleksiiStepanov.UI
{
    public class GameplayPanel : UIPanel
    {
        [Header("Content")]
        [SerializeField] private ComboCounter comboCounter;
        [SerializeField] private List<GameplayPanelLayout> layouts;
        [SerializeField] private Transform backButtonTransform;
        
        [Header("Message Text")]
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private RectTransform messageTextRectTransform;

        private GameplayPanelLayout _currentLayout;
        
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
            messageText.text = Constants.GAMEPLAY_MESSAGE_GO;
            AnimateMessageText(() =>
            {
                _currentLayout.StartGame();
            });
        }
        
        public override void OnUIPanelOpened()
        {
            backButtonTransform.gameObject.SetActive(true);
            backButtonTransform.DOShakeScale(0.2f, 0.5f);
            
            _currentLayout.PlayShakeAnimation();
        }

        public void OnBackButtonClicked()
        {
            backButtonTransform.gameObject.SetActive(false);
            
            _currentLayout.StopGame();
            UIManager.Instance.OpenLevelPanel();
        }

        private void AnimateMessageText(Action onComplete = null)
        {
            messageTextRectTransform.gameObject.SetActive(true);
            messageTextRectTransform.anchoredPosition = new Vector2(0, -2000);
            
            var sequence = DOTween.Sequence();

            sequence.Append(messageTextRectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.Linear))
                .AppendInterval(1f)
                .Append(messageTextRectTransform.DOAnchorPosY(2000, 0.5f))
                .AppendCallback(() =>
                {
                    messageTextRectTransform.gameObject.SetActive(false); 
                    onComplete?.Invoke();
                });
        }

        private void HideAllLayouts()
        {
            foreach (var layout in layouts)
            {
                layout.gameObject.SetActive(false);
            }
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

        private void OnWin()
        {
            backButtonTransform.gameObject.SetActive(false);
            
            messageText.text = Constants.GAMEPLAY_MESSAGE_YOU_WON;
            
            AnimateMessageText(OnBackButtonClicked);
        }

        private void OnBingoSequenceFinished()
        {
            backButtonTransform.gameObject.SetActive(false);
            
            messageText.text = Constants.GAMEPLAY_MESSAGE_ROUND_OVER;
            
            AnimateMessageText(OnBackButtonClicked);
        }
    }    
}

