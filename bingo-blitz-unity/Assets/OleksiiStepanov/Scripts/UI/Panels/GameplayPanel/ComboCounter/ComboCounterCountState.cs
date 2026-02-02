using System;
using BingoBlitzClone.Gameplay;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BingoBlitzClone.UI
{
    public class ComboCounterCountState : MonoBehaviour
    {
        [Header("Counter State")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image barImage;
        [SerializeField] private Image overlayImage;
        
        private Tweener _counterTweener;
        private GameRules _gameRules;
        
        private float _barComboFillStep;

        [Inject]
        public void Construct(GameRules gameRules)
        {
            _gameRules = gameRules;
            _barComboFillStep = 1f / gameRules.MaxCombo;
        }

        public void Init(Action onComplete = null)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.5f);
            
            barImage.fillAmount = 0;
            overlayImage.fillAmount = 0;
            
            onComplete?.Invoke();
        }

        public void UpdateFill(int comboCount, Action onComplete)
        {
            if (_counterTweener != null && _counterTweener.IsActive())
            {
                _counterTweener.Kill();
            }
            
            float currentFillAmount = barImage.fillAmount;
            float targetFillAmount = _barComboFillStep * comboCount;

            _counterTweener = DOVirtual
                .Float(currentFillAmount, targetFillAmount, 0.3f, value => barImage.fillAmount = value).OnComplete(
                    () =>
                    {
                        onComplete?.Invoke();
                    });
        }

        public void ResetState()
        {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(false);
        }
    }
}