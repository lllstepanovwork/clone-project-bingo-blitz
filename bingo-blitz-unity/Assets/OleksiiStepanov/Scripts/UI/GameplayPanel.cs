using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace OleksiiStepanov.UI
{
    public class GameplayPanel : UIPanel
    {
        [Header("Content")] 
        [SerializeField] private List<GameplayPanelLayout> layouts;

        [Header("Go Text Image")] 
        [SerializeField] private RectTransform goTextImageRectTransform;
        [SerializeField] private Image goTextImage;

        private GameplayPanelLayout currentLayout;
        
        public void Init(int numberOfFields)
        {
            HideAllLayouts();

            currentLayout = layouts[numberOfFields-1];
            currentLayout.gameObject.SetActive(true);
            currentLayout.Init();
        }

        public void StartGame()
        {
            AnimateGoTextImage(() =>
            {
                currentLayout.StartGame();
            });
        }

        public override void OnUIPanelOpened()
        {
            currentLayout.PlayShakeAnimation();
        }

        private void AnimateGoTextImage(Action onComplete = null)
        {
            goTextImage.gameObject.SetActive(true);
            goTextImageRectTransform.anchoredPosition = new Vector2(0, -2000);
            
            Sequence seq = DOTween.Sequence();

            seq.Append(goTextImageRectTransform.DOAnchorPos(Vector2.zero, 0.25f))
                .Join(goTextImage.DOFade(1, 0.1f))
                .AppendInterval(1f)
                .Append(goTextImageRectTransform.DOAnchorPosY(2000, 0.5f))
                .Join(goTextImage.DOFade(0, 1f))
                .AppendCallback(() =>
                {
                    goTextImage.gameObject.SetActive(false); 
                    onComplete?.Invoke();
                });
        }

        private void HideAllLayouts()
        {
            foreach (var layout in layouts)
            {
                layout.gameObject.SetActive(false);
            }
        }
    }    
}

