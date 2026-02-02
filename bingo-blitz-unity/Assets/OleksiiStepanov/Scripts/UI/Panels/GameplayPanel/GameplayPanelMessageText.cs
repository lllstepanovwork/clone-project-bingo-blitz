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
        
        public void ShowMessage(string message, Action onComplete)
        {
            messageText.text = message;
            AnimateMessageText(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        private void AnimateMessageText(Action onComplete = null)
        {
            messageTextRectTransform.gameObject.SetActive(true);
            messageTextRectTransform.anchoredPosition = new Vector2(0, -2000);
            
            var sequence = DOTween.Sequence();

            sequence.Append(messageTextRectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.Linear))
                .AppendInterval(1f)
                .Append(messageTextRectTransform.DOAnchorPosY(2000, 0.5f))
                .AppendCallback(() =>
                {
                    messageTextRectTransform.gameObject.SetActive(false); 
                    onComplete?.Invoke();
                });
        }
    }
}