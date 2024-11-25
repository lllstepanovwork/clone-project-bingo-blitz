using System;
using DG.Tweening;
using OleksiiStepanov.Game;
using OleksiiStepanov.UI;
using UnityEngine;

namespace MyNamespace
{
    public class AboutPanel : UIPanel
    {
        private event Action OnContinue;

        public void Init(Action onContinueAction)
        {
            OnContinue = onContinueAction;
        }

        public void OnOleksiiStepanovLinkClicked()
        {
            Application.OpenURL(Constants.OLEKSII_STEPANOV_LINK);
        }
        
        public void OnPlaytikaLinkClicked()
        {
            Application.OpenURL(Constants.PLAYTIKA_LINK);
        }
        
        public void OnBingoBlitzLinkClicked()
        {
            Application.OpenURL(Constants.BINGO_BLITZ_LINK);
        }
        
        public void OnContinueButtonClick()
        {
            OnContinue?.Invoke();
        }

        public override void OnUIPanelOpened()
        {
            transform.DOShakeScale(0.2f, 0.2f);
        }
    }
}
