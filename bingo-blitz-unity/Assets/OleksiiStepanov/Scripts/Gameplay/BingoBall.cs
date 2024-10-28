using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using OleksiiStepanov.Game;

namespace OleksiiStepanov.Gameplay
{
    public class BingoBall : MonoBehaviour
    {
        [Header ("Content")]
        [SerializeField] private Image ballImage;
        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform mainRectTransform;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text numberText;
        
        private bool _initialized = false;
        public void Init(int number, Sprite ballSprite)
        {
            _initialized = true;
         
            mainRectTransform.localScale = Vector3.one;
            numberText.text = number.ToString();
            ballImage.sprite = ballSprite;
            
            canvasGroup.DOFade(1f, 0.5f);
        }

        public void ScaleDown()
        {
            mainRectTransform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f);
        }

        public void Hide(BingoSequenceTransparencyType bingoSequenceTransparencyType)
        {
            if (!_initialized)
            {
                return;
            }

            var transparency = bingoSequenceTransparencyType switch
            {
                BingoSequenceTransparencyType.Full => 1f,
                BingoSequenceTransparencyType.Quarter => 0.75f,
                BingoSequenceTransparencyType.Half => 0.5f,
                BingoSequenceTransparencyType.Clear => 0.5f,
                _ => 1.0f
            };

            canvasGroup.DOFade(transparency, 0.5f);
        }
    }
}

