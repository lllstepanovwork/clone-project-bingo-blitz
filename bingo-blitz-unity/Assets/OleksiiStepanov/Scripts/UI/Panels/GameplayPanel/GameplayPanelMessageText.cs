using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace BingoBlitzClone.UI
{
    public class GameplayPanelMessageText : MonoBehaviour
    {
        [Header("Message Text")]
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private RectTransform messageTextRectTransform;
        
        private Sequence _sequence;
        
        public void ShowMessage(string message, Action onComplete)
        {
            messageText.text = message;
            
            _sequence?.Kill();
            
            AnimateMessageText(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        
        private void AnimateMessageText(Action onComplete = null)
        {
            messageTextRectTransform.gameObject.SetActive(true);
            messageTextRectTransform.anchoredPosition = new Vector2(0, -2000);
            
            _sequence = DOTween.Sequence();

            _sequence.Append(messageTextRectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.Linear))
                .AppendInterval(1f)
                .Append(messageTextRectTransform.DOAnchorPosY(2000, 0.5f))
                .AppendCallback(() =>
                {
                    messageTextRectTransform.gameObject.SetActive(false); 
                    onComplete?.Invoke();
                });
        }
        
        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}