using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BingoBlitzClone.UI
{
    public class ComboCounterRewardState : MonoBehaviour
    {
        [Header("Reward State")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform starRectTransform;
        [SerializeField] private Image gradientImage;
        [SerializeField] private Image circleFXImage;

        private Sequence _rewardStarSequence;
        private Sequence _rewardGradientSequence;

        public void Init(Action onComplete = null)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.5f);
            
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
        
        public void PlayRewardStateClickAnimation(Action onComplete)
        {
            _rewardStarSequence?.Kill();
            _rewardGradientSequence?.Kill();
            
            circleFXImage.transform.localScale = Vector3.one;
            
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(circleFXImage.DOFade(1f, 0.15f))
                .Join(circleFXImage.transform.DOScale(1.15f, 0.15f))
                .Append(circleFXImage.DOFade(0f, 0.15f))
                .Join(circleFXImage.transform.DOScale(1.3f, 0.15f));
                
            onComplete?.Invoke();
        }

        public void ResetState()
        {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(false);
        }
    }
}