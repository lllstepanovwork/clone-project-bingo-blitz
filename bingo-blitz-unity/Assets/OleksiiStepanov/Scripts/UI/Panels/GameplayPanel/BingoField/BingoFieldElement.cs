using UnityEngine;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldElement : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private BingoFieldElementState activeState;
        [SerializeField] private BingoFieldElementState doneState;
        [SerializeField] private BingoFieldElementState combinationState;
        
        public bool Done { get; private set; }
        public int Number { get; private set; }
        
        private bool _initialized = false;
        private BingoField _bingoField;
        
        public void Init(BingoField bingoField, int bingoNumber)
        {
            _bingoField = bingoField;
            
            Done = false;
            Number = bingoNumber;
            
            activeState.Enter();
            activeState.SetNumber(bingoNumber);
            
            _initialized = true;
        }

        public void SetAsDone()
        {
            Done = true;
            
            activeState.Exit();
            doneState.Enter();
        }
        
        public void SetAsCombinationState()
        {
            doneState.Exit();
            combinationState.Enter();
        }

        public void OnActiveStateButtonClick()
        {
            if (!_initialized)
            {
                return;
            }

            _bingoField.OnBingoFieldElementButtonClick(this);
        }
        
        public void OnCombinationStateButtonClick()
        {
            _bingoField.SetAsDone();
        }
    }
}

