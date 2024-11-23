using DG.Tweening;
using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class LoadingPanel : UIPanel
    {
        [Header("Logo")] 
        [SerializeField] private RectTransform logoRectTransform;
        [SerializeField] private RectTransform lightRectTransform;
        [SerializeField] private RectTransform planetRectTransform;
        
        [Header("LoadingBar")]
        [SerializeField] private CanvasGroup loadingBarCanvasGroup;
        [SerializeField] private RectTransform loadingBarRectTransform;

        private Tweener _logoTweener;
        private Tweener _planetTweener;
        private Tweener _lightTweener;
        
        public override void OnUIPanelOpened()
        {
            Init();
        }

        private void Init()
        {
            PlayLogoAnimation();
            
            EnableLoadingBar();
        }

        private void EnableLoadingBar()
        {
            loadingBarCanvasGroup.gameObject.SetActive(true);
            loadingBarCanvasGroup.DOFade(1, 0.5f).onComplete = () =>
            {
                DOVirtual.Vector2(loadingBarRectTransform.offsetMax, Vector2.zero, 3f, (value) =>
                {
                    loadingBarRectTransform.offsetMax = value;
                }).onComplete = () =>
                {
                    KillLogoAnimation();
                    UIManager.Instance.OpenLevelPanel();
                };
            };
        }

        private void PlayLogoAnimation()
        {
            _logoTweener = logoRectTransform.DOScale(new Vector3(1.1f, 1.1f, 1), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            _planetTweener = planetRectTransform.DOScale(new Vector3(1.1f, 1.1f, 1), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            _lightTweener = lightRectTransform.DORotate(new Vector3(0, 0, 180), 30f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }

        private void KillLogoAnimation()
        {
            _logoTweener?.Kill();
            _planetTweener?.Kill();
            _lightTweener?.Kill();
        }
    }
}
