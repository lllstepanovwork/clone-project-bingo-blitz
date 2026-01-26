using UnityEngine.UI;
using UnityEngine;
using System;
using BingoBlitzClone.Game;
using BingoBlitzClone.Gameplay;
using DG.Tweening;

namespace BingoBlitzClone.UI
{
    public class GameplayPanelComboCounter : MonoBehaviour
    {
        [Header("Counter State")] 
        [SerializeField] private CanvasGroup counterStateCanvasGroup;
        [SerializeField] private Image counterBarImage;
        [SerializeField] private Image counterOverlayImage;
        
        [Header("Reward State")] 
        [SerializeField] private CanvasGroup rewardStateCanvasGroup;
        [SerializeField] private RectTransform starRectTransform;
        [SerializeField] private Image gradientImage;
        [SerializeField] private Image circleFXImage;
        
        [Header("Cooldown State")] 
        [SerializeField] private CanvasGroup cooldownStateCanvasGroup;
        [SerializeField] private Image cooldownBarImage;
        [SerializeField] private Image cooldownOverlayImage;
        
        public static event Action OnReward;
        
        private int _currentComboCounter = 0;
        private const int MaxComboCounter = 3;
        private const float BarComboFillAmount = 0.33f;
        
        private Tweener _counterTweener;
        private Sequence _rewardStarSequence;
        private Sequence _rewardGradientSequence;
        private Tweener _cooldownTweener;
        
        private ComboCounterState _currentComboCounterState;

        public void Init()
        {
            ShowState(ComboCounterState.CounterState);
        }
        
        private void ShowState(ComboCounterState state)
        {
            HideAllStates();
            
            _currentComboCounterState = state;

            switch (state)
            {
                case ComboCounterState.CounterState:
                    InitCounterState();
                    break;
                case ComboCounterState.RewardState:
                    InitRewardState();
                    break;
                case ComboCounterState.CooldownState:
                    InitCooldownState();
                    break;
            }
        }

        private void OnMatch()
        {
            if (_currentComboCounterState != ComboCounterState.CounterState)
            {
                return;
            }

            if (_counterTweener != null && _counterTweener.IsActive())
            {
                _counterTweener.Kill();
            }

            _currentComboCounter++;
            
            float currentFillAmount = counterBarImage.fillAmount;
            float targetFillAmount = BarComboFillAmount * _currentComboCounter;

            _counterTweener = DOVirtual
                .Float(currentFillAmount, targetFillAmount, 0.3f, value => counterBarImage.fillAmount = value).OnComplete(
                    () =>
                    {
                        if (_currentComboCounter == MaxComboCounter)
                        {
                            _currentComboCounter = 0;

                            ShowState(ComboCounterState.RewardState);
                        }
                    });
        }

        private void InitCounterState()
        {
            counterStateCanvasGroup.gameObject.SetActive(true);
            counterStateCanvasGroup.DOFade(1f, 0.5f);
            
            _currentComboCounter = 0;
            
            counterBarImage.fillAmount = 0;
            counterOverlayImage.fillAmount = 0;
        }
        
        private void InitRewardState()
        {
            rewardStateCanvasGroup.gameObject.SetActive(true);
            rewardStateCanvasGroup.DOFade(1f, 0.5f);
            
            _rewardStarSequence = DOTween.Sequence();
            _rewardGradientSequence = DOTween.Sequence();
            
            starRectTransform.localScale = Vector3.one;
            gradientImage.DOFade(1f, 0f);

            _rewardStarSequence
                .Append(starRectTransform.DOScale(1f, 0.2f))
                .Append(starRectTransform.DOScale(0.9f, 0.2f))
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            
            _rewardGradientSequence
                .Join(gradientImage.DOFade(0.5f, 0.25f))
                .Join(gradientImage.DOFade(1f, 0.25f))
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        
        private void InitCooldownState()
        {
            cooldownStateCanvasGroup.gameObject.SetActive(true);
            cooldownStateCanvasGroup.DOFade(1f, 0.5f);
            cooldownOverlayImage.fillAmount = 1;
            cooldownBarImage.fillAmount = 1;

            _cooldownTweener = DOVirtual.Float(cooldownBarImage.fillAmount, 0, 5f, value =>
                {
                    cooldownOverlayImage.fillAmount = value;
                    cooldownBarImage.fillAmount = value;
                })
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    transform.DOShakeScale(0.2f, 0.5f);
                    
                    _cooldownTweener?.Kill();
                    ShowState(ComboCounterState.CounterState);
                });
        }

        private void HideAllStates()
        {
            counterStateCanvasGroup.alpha = 0;
            rewardStateCanvasGroup.alpha = 0;
            cooldownStateCanvasGroup.alpha = 0;
            
            counterStateCanvasGroup.gameObject.SetActive(false);
            rewardStateCanvasGroup.gameObject.SetActive(false);
            cooldownStateCanvasGroup.gameObject.SetActive(false);
        }

        public void OnRewardButtonClicked()
        {
            _rewardStarSequence?.Kill();
            _rewardGradientSequence?.Kill();
            
            PlayRewardStateClickAnimation(()=>
            {
                ShowState(ComboCounterState.CooldownState);    
            });
            
            OnReward?.Invoke();            
        }

        private void PlayRewardStateClickAnimation(Action onComplete)
        {
            circleFXImage.transform.localScale = Vector3.one;
            
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(circleFXImage.DOFade(1f, 0.15f))
                .Join(circleFXImage.transform.DOScale(1.15f, 0.15f))
                .Append(circleFXImage.DOFade(0f, 0.15f))
                .Join(circleFXImage.transform.DOScale(1.3f, 0.15f));
                
            onComplete?.Invoke();
        }

        private void OnEnable()
        {
            BingoField.OnMatch += OnMatch;
        }
        
        private void OnDisable()
        {
            BingoField.OnMatch -= OnMatch;
        }
    }    
}

