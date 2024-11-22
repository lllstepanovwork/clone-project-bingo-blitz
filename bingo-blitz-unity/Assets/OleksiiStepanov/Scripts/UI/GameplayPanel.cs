using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class GameplayPanel : UIPanel
    {
        [Header("Content")] 
        [SerializeField] private List<GameplayPanelLayout> layouts;
        [SerializeField] private Transform backButtonTransform;

        [Header("Go Text Image")] 
        [SerializeField] private RectTransform goTextImageRectTransform;
        [SerializeField] private RectTransform gameOverTextTransform;

        private GameplayPanelLayout _currentLayout;
        
        public void Init(int numberOfFields)
        {
            HideAllLayouts();

            _currentLayout = layouts[numberOfFields-1];
            _currentLayout.gameObject.SetActive(true);
            _currentLayout.Init();
        }

        public void StartGame()
        {
            AnimateTextMessage(goTextImageRectTransform, () =>
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

        private void AnimateTextMessage(RectTransform textMessageRectTransform, Action onComplete = null)
        {
            textMessageRectTransform.gameObject.SetActive(true);
            textMessageRectTransform.anchoredPosition = new Vector2(0, -2000);
            
            var sequence = DOTween.Sequence();

            sequence.Append(textMessageRectTransform.DOAnchorPos(Vector2.zero, 0.75f).SetEase(Ease.Linear))
                .AppendInterval(1f)
                .Append(textMessageRectTransform.DOAnchorPosY(2000, 0.5f))
                .AppendCallback(() =>
                {
                    textMessageRectTransform.gameObject.SetActive(false); 
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
            GameplayPanelLayout.OnGameOver += OnGameOver;
        }
        
        private void OnDisable()
        {
            GameplayPanelLayout.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            backButtonTransform.gameObject.SetActive(false);
            
            AnimateTextMessage(gameOverTextTransform, OnBackButtonClicked);
        }
    }    
}

