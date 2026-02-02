using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldElementDoneState : BingoFieldElementState
    {
        [Header("Content")]
        [SerializeField] private Transform starTransform;
        [SerializeField] private Image doneStateBackgroundImage;
        
        public override void Enter()
        {
            gameObject.SetActive(true);
            
            AnimateDoneState();
        }
        
        private void AnimateDoneState()
        {
            var startPixelPerUnitMultiplierValue = doneStateBackgroundImage.pixelsPerUnitMultiplier;
            var pixelPerUnitMultiplierMaxValue = startPixelPerUnitMultiplierValue + 2f;
            
            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(DOVirtual.Float(startPixelPerUnitMultiplierValue, pixelPerUnitMultiplierMaxValue, 0.1f, v => doneStateBackgroundImage.pixelsPerUnitMultiplier = v))
                .Append(DOVirtual.Float(pixelPerUnitMultiplierMaxValue, startPixelPerUnitMultiplierValue, 0.1f, v => doneStateBackgroundImage.pixelsPerUnitMultiplier = v))
                .Join(starTransform.DOScale(1.2f, 0.1f))
                .Append(starTransform.DOScale(1, 0.1f))
                .Append(starTransform.DOShakePosition(0.1f, 0.2f))
                .Join(starTransform.DOShakeScale(0.1f, 0.2f));
        }

        public override void Exit()
        {
            gameObject.SetActive(false);
        }
    }
}