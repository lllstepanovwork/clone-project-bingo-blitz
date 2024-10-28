using System;
using UnityEngine;
using TMPro;

namespace OleksiiStepanov.Gameplay
{
    public class BingoFieldElement : MonoBehaviour
    {
        public static event Action<BingoFieldElement> OnButtonClick;

        [Header("Content")]
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject doneState;
        [SerializeField] private TMP_Text numberText;

        public bool Done { get; private set; }
        
        public int Number { get; private set; }
        private bool _initialized = false;
        
        public void Init(int bingoNumber)
        {
            Number = bingoNumber;
        
            numberText.text = Number.ToString();
            
            _initialized = true;
        }

        public void SetAsDone()
        {
            activeState.SetActive(false);
            doneState.SetActive(true);

            Done = true;
        }

        public void ClickButton()
        {
            if (!_initialized)
            {
                return;
            }

            OnButtonClick?.Invoke(this);
        }
    }    
}

