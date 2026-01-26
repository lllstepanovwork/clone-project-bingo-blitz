using System;
using DG.Tweening;

namespace BingoBlitzClone.UI
{
    public class FadeUIViewAnimation : IUIViewAnimation
    {
        private const float FadeDuration = 0.3f;
        
        public void Animate(UIPanel uIPanel, bool open, Action onComplete = null)
        {
            uIPanel.canvasGroup.DOKill();

            float startAlpha = open ? 0f : 1f;
            float endAlpha   = open ? 1f : 0f;

            uIPanel.canvasGroup.alpha = startAlpha;
            uIPanel.canvasGroup.blocksRaycasts = open;
            uIPanel.canvasGroup.interactable = open;

            uIPanel.canvasGroup
                .DOFade(endAlpha, FadeDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    uIPanel.canvasGroup.blocksRaycasts = open;
                    uIPanel.canvasGroup.interactable = open;
                    onComplete?.Invoke();
                });
        }
    }
}