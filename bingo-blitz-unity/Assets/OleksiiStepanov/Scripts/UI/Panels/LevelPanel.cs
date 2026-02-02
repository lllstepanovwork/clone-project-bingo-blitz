using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BingoBlitzClone.UI
{
    public class LevelPanel : UIPanel
    {
        [Header("Content")]
        [SerializeField] private Button plusButton;
        [SerializeField] private Button minusButton;
        [SerializeField] private TMP_Text layoutNumberText;
        
        private int _layoutNumber = 1;
        private const int MinLayoutNumber = 1;
        private const int MaxLayoutNumber = 4;

        private int LayoutNumber
        {
            get => _layoutNumber;
            set
            {
                _layoutNumber = Mathf.Clamp(value, MinLayoutNumber, MaxLayoutNumber);
                UpdateLayoutText();
                UpdateButtonStates();
            }
        }
        
        private UIManager _uiManager;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus, UIManager uiManager)
        {
            _uiManager = uiManager;
            _signalBus = signalBus;
        }

        private void Init()
        {
            LayoutNumber = MinLayoutNumber;
            UpdateLayoutText();
        }
        
        public override void OnUIPanelOpened()
        {
            PlayShakeAnimation();
            
            Init();
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
            plusButton.interactable = LayoutNumber < MaxLayoutNumber;
            minusButton.interactable = LayoutNumber > MinLayoutNumber;
        }

        public void OnPlayButtonClicked()
        {
            _uiManager.OpenGameplayPanel(LayoutNumber);
            
            _signalBus.Fire(new LayoutSelectedSignal(LayoutNumber));
        }
    }

    public class LayoutSelectedSignal
    {
        public readonly int LayoutNumber;
        
        public LayoutSelectedSignal(int layoutNumber)
        {
            LayoutNumber = layoutNumber;
        }
    }
}
