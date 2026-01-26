using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldElement : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject doneState;
        [SerializeField] private GameObject combinationState;
        [SerializeField] private TMP_Text numberText;
        
        [Header("DoneStateAnimation")]
        [SerializeField] private Transform starTransform;
        [SerializeField] private Image doneStateBackgroundImage;
        
        public bool Done { get; private set; }
        public int Number { get; private set; }
        
        private bool _initialized = false;
        
        private BingoField _bingoField;
        
        public void Init(BingoField bingoField, int bingoNumber)
        {
            _bingoField = bingoField;
            
            Done = false;
            Number = bingoNumber;
            
            activeState.SetActive(true);
            doneState.SetActive(false);
            combinationState.SetActive(false);
        
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
        
        public void SetAsCombinationState()
        {
            doneState.SetActive(false);
            combinationState.SetActive(true);

            combinationState.transform.DOShakeScale(0.5f, 0.2f);
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

        public void OnActiveStateButtonClick()
        {
            if (!_initialized)
            {
                return;
            }

            _bingoField.OnBingoFieldElementButtonClick(this);
        }
        
        public void OnCombinationStateButtonClick()
        {
            _bingoField.SetAsDone();
        }
    }    
}

