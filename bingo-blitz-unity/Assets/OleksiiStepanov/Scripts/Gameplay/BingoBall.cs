using DG.Tweening;
using OleksiiStepanov.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        
        private bool _showed = false;
        
        public void Init()
        {
            _showed = false;
            canvasGroup.alpha = 0;
        }

        public void Show(int number, Sprite ballSprite)
        {
            _showed = true;
         
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
            if (!_showed)
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

