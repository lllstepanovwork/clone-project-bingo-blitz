using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;

namespace OleksiiStepanov.Gameplay
{
    public class BingoFieldElement : MonoBehaviour
    {
        public static event Action<BingoFieldElement> OnButtonClick;

        [Header("Content")]
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject doneState;
        [SerializeField] private TMP_Text numberText;
        
        [Header("DoneStateAnimation")]
        [SerializeField] private Transform starTransform;
        [SerializeField] private Image doneStateBackgroundImage;
        
        public bool Done { get; private set; }
        
        public int Number { get; private set; }
        private bool _initialized = false;
        
        public void Init(int bingoNumber)
        {
            Number = bingoNumber;
        
            numberText.text = Number.ToString();
            
            _initialized = true;
        }

        public void SetAsDone()
        {
            activeState.SetActive(false);
            doneState.SetActive(true);

            Done = true;
            
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

        public void ClickButton()
        {
            if (!_initialized)
            {
                return;
            }

            OnButtonClick?.Invoke(this);
        }
    }    
}

