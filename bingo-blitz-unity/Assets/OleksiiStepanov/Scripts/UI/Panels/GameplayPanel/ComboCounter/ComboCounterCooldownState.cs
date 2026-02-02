using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BingoBlitzClone.UI
{
    public class ComboCounterCooldownState : MonoBehaviour
    {
        [Header("Cooldown State")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image barImage;
        [SerializeField] private Image overlayImage;
        
        private Tweener _cooldownTweener;
        
        public void Init(Action onComplete = null)
        {
            if (_cooldownTweener != null && _cooldownTweener.IsActive())
            {
                _cooldownTweener.Kill();
            }
            
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.5f);
            overlayImage.fillAmount = 1;
            barImage.fillAmount = 1;

            _cooldownTweener = DOVirtual.Float(barImage.fillAmount, 0, 5f, value =>
                {
                    overlayImage.fillAmount = value;
                    barImage.fillAmount = value;
                })
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    transform.DOShakeScale(0.2f, 0.5f);
                    
                    _cooldownTweener?.Kill();
                    
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