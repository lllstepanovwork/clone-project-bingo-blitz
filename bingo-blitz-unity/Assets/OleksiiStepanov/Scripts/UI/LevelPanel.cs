using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace OleksiiStepanov.UI
{
    public class LevelPanel : UIPanel
    {
        [Header("Content")]
        [SerializeField] private Button plusButton;
        [SerializeField] private Button minusButton;
        [SerializeField] private TMP_Text layoutNumberText;
        
        private int _layoutNumber = 1;
        private const int MIN_LAYOUT_NUMBER = 1;
        private const int MAX_LAYOUT_NUMBER = 4;

        private int LayoutNumber
        {
            get => _layoutNumber;
            set
            {
                _layoutNumber = Mathf.Clamp(value, MIN_LAYOUT_NUMBER, MAX_LAYOUT_NUMBER);
                UpdateLayoutText();
                UpdateButtonStates();
            }
        }

        public void Init()
        {
            LayoutNumber = MIN_LAYOUT_NUMBER;
        }
        
        public override void OnUIPanelOpened()
        {
            PlayShakeAnimation();
        }

        private void PlayShakeAnimation()
        {
            contentTransform.DOShakeScale(.2f, .2f);
        }

        public void OnNextLayoutNumberButtonClicked()
        {
            LayoutNumber++;
        }

        public void OnPrevLayoutNumberButtonClicked()
        {
            LayoutNumber--;
        }

        private void UpdateLayoutText()
        {
            layoutNumberText.text = LayoutNumber.ToString();
        }

        private void UpdateButtonStates()
        {
            plusButton.interactable = LayoutNumber < MAX_LAYOUT_NUMBER;
            minusButton.interactable = LayoutNumber > MIN_LAYOUT_NUMBER;
        }

        public void OnPlayButtonClicked()
        {
            Debug.Log(LayoutNumber);
            UIManager.Instance.OpenGameplayPanel(LayoutNumber);
        }
    }    
}
