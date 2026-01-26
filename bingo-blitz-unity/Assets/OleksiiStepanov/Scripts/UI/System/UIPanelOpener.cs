using System;

namespace BingoBlitzClone.UI
{
    public class UIPanelOpener
    {
        public void OpenPanel(UIPanel uIPanel, Action onComplete = null)
        {
            uIPanel.gameObject.SetActive(true);

            IUIViewAnimation animation = GetAnimation(uIPanel.animationType);

            if (animation == null)
            {
                uIPanel.OnUIPanelOpened();
                onComplete?.Invoke();
                return;
            }

            animation.Animate(uIPanel, true, () =>
            {
                uIPanel.OnUIPanelOpened();
                onComplete?.Invoke();
            });
        }

        public void ClosePanel(UIPanel uIPanel, Action onComplete = null)
        {
            IUIViewAnimation animation = GetAnimation(uIPanel.animationType);

            if (animation == null)
            {
                uIPanel.gameObject.SetActive(false);
                onComplete?.Invoke();
                return;
            }

            animation.Animate(uIPanel, false, () =>
            {
                uIPanel.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
        
        private IUIViewAnimation GetAnimation(UIViewAnimationType animationType)
        {
            switch (animationType)
            {
                case UIViewAnimationType.Fade:
                    return new FadeUIViewAnimation();

                case UIViewAnimationType.FromBottom:
                    return new FromBottomUIViewAnimation();

                case UIViewAnimationType.None:
                default:
                    return null;
            }
        }
    }
}


