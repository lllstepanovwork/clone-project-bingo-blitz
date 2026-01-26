using System;
using DG.Tweening;
using UnityEngine;

namespace BingoBlitzClone.UI
{
    public class FromBottomUIViewAnimation : IUIViewAnimation
    {
        private const float Duration = 0.5f;
        private const float OffsetY = 200f;

        public void Animate(UIPanel panel, bool open, Action onComplete = null)
        {
            RectTransform rect = panel.contentTransform as RectTransform;

            rect.DOKill();

            float startY = open ? -OffsetY : 0f;
            float endY   = open ? 0f : -OffsetY;

            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, startY);

            rect.DOAnchorPosY(endY, Duration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}