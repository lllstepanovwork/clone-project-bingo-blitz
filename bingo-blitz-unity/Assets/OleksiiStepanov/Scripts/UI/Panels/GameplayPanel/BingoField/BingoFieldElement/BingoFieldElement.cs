using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    public class BingoFieldElement : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private BingoFieldElementState activeState;
        [SerializeField] private BingoFieldElementState doneState;
        [SerializeField] private BingoFieldElementState combinationState;
        
        public bool Done { get; private set; }
        
        private int _number;
        private int _bingoFieldId;
        
        private bool _initialized;
        
        private SignalBus _signalBus;
        private BingoLogic _bingoLogic;
        
        [Inject]
        public void Construct(SignalBus signalBus, BingoLogic bingoLogic)
        {
            _signalBus = signalBus;
            _bingoLogic = bingoLogic;
        }

        public void Init(int bingoFieldId, int bingoNumber)
        {
            ResetStates();
            
            Done = false;
            
            _number = bingoNumber;
            _bingoFieldId = bingoFieldId;
            
            activeState.Enter();
            activeState.SetNumber(bingoNumber);
            
            _initialized = true;
        }

        private void ResetStates()
        {
            activeState.ResetState();
            doneState.ResetState();
            combinationState.ResetState();
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
                return;

            _bingoLogic.AddNumberToCurrentCombination(_number, SetAsDone);
        }
        
        public void OnCombinationStateButtonClick()
        {
            _signalBus.Fire(new CompleteFieldButtonClickedSignal(_bingoFieldId));
        }

        public class CompleteFieldButtonClickedSignal
        {
            public int BingoFieldId { get; private set; }

            public CompleteFieldButtonClickedSignal(int bingoFieldId)
            {
                BingoFieldId = bingoFieldId;
            }
        }
    }
}

