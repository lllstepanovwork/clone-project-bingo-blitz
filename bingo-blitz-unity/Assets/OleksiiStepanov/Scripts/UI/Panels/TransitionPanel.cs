using System;
using UnityEngine;
using DG.Tweening;

namespace BingoBlitzClone.UI
{
    public class TransitionPanel : UIPanel
    {
        [Header("Content")] 
        [SerializeField] private RectTransform leftSide;
        [SerializeField] private RectTransform rightSide;

        public void Init(Action onStart, Action onMiddlePoint = null, Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .AppendCallback(() =>
                {
                    onStart?.Invoke();       
                })
                .Append(leftSide.DOAnchorPosX(0, 0.5f))
                .Join(rightSide.DOAnchorPosX(0, 0.5f))
                .AppendInterval(0.5f)
                .AppendCallback(() =>
                {
                    onMiddlePoint?.Invoke();       
                })
                .Append(leftSide.DOAnchorPosX(-2000, 0.5f))
                .Join(rightSide.DOAnchorPosX(2000, 0.5f))
                .AppendCallback(() =>
                {
                    onComplete?.Invoke();       
                });
    }

        public override void OnUIPanelOpened()
        {
            
        }
    }
}
