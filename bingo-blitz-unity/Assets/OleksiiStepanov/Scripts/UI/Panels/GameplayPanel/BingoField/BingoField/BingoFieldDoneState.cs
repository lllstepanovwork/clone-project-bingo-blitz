using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldDoneState : BingoFieldState
    {
        [Header("Content")]
        [SerializeField] private Image doneStateBackground;
        [SerializeField] private List<RectTransform> bingoLetters = new List<RectTransform>();
        
        public override void Enter(Action onComplete = null)
        {
            gameObject.SetActive(true);
            
            foreach (var bingoLetter in bingoLetters)
            {
                bingoLetter.localScale = Vector3.zero;
            }
            
            onComplete?.Invoke();
        }

        public override void Exit(Action onComplete = null)
        {
            PlayDoneStateAnimation(onComplete);
        }

        public override void ResetState()
        {
            gameObject.SetActive(false);
        }

        private void PlayDoneStateAnimation(Action onAnimationComplete = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(doneStateBackground.DOFade(1, 0.1f));

            foreach (var letter in bingoLetters)
            {
                sequence.Append(letter.DOScale(1.5f, 0.065f));
                sequence.Append(letter.DOScale(1f, 0.065f));
            }

            sequence.AppendCallback(() =>
            {
                onAnimationComplete?.Invoke();
            });
        }
    }
}