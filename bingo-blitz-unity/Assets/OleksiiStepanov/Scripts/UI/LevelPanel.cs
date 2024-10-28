using UnityEngine;
using System.Collections.Generic;
using OleksiiStepanov.UI;
using TMPro;
using UnityEngine.UI;

namespace OleksiiStepanov.UI
{
    public class LevelPanel : UIPanel
    {
        [SerializeField] private Button plusButton;
        [SerializeField] private Button minusButton;
        [SerializeField] private TMP_Text layoutNumberText;
        
        public GameplayPanel gameplayPanel;

        private int layoutNumber = 1;
        private int minLayoutNumber = 1;
        private int maxLayoutNumber = 4;
        
        public void Init()
        {
            layoutNumber = 1;
            UpdateLayoutText();
        }

        public void OnNextLayoutNumberButtonClicked()
        {
            layoutNumber++;

            if (layoutNumber == maxLayoutNumber)
            {
                plusButton.interactable = false;
            }
            
            minusButton.interactable = true;

            UpdateLayoutText();
        }
        
        public void OnPrevLayoutNumberButtonClicked()
        {
            layoutNumber--;

            if (layoutNumber == minLayoutNumber)
            {
                minusButton.interactable = false;
            } 
            
            plusButton.interactable = true;

            UpdateLayoutText();
        }

        private void UpdateLayoutText()
        {
            layoutNumberText.text = layoutNumber.ToString();
        }

        public void OnPlayButtonClicked()
        {
            UIManager.Instance.OpenGameplayPanel(layoutNumber);
        }
    }    
}