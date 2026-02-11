using System.Collections.Generic;
using BingoBlitzClone.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BingoBlitzClone.Gameplay
{
    public class BingoField : MonoBehaviour
    {
        [Header("Content")] 
        [SerializeField] private Transform contentTransform;
        
        [Header("States")] 
        [SerializeField] private BingoFieldState activeState;
        [SerializeField] private BingoFieldState doneState;

        [Header("Columns")]
        [SerializeField] private List<BingoFieldColumn> columns;
        
        private readonly List<BingoFieldElement> _elements = new List<BingoFieldElement>();
        
        private SignalBus _signalBus;
        private BingoLogic _bingoLogic;
        
        private readonly Combination _currentCombination = new Combination();
        
        private bool _active;
        private bool _done;
        private int _bingoFieldId;
        
        [Inject]
        public void Construct(SignalBus signalBus, BingoLogic bingoLogic)
        {
            _signalBus = signalBus;
            _bingoLogic = bingoLogic;
        }
        
        private void OnEnable()
        {
            _active = true;
            
            _signalBus.Subscribe<BingoFieldElement.CompleteFieldButtonClickedSignal>(SetAsDone);
            _signalBus.Subscribe<BingoLogic.NumberMatchSignal>(CheckBingoCombinations);
        }

        private void OnDisable()
        {
            _active = false;
            
            _signalBus.Unsubscribe<BingoFieldElement.CompleteFieldButtonClickedSignal>(SetAsDone);
            _signalBus.Unsubscribe<BingoLogic.NumberMatchSignal>(CheckBingoCombinations);
        }

        public void Init(int bingoFieldId)
        {
            _bingoFieldId = bingoFieldId;
            
            activeState.ResetState();
            doneState.ResetState();
            
            activeState.Enter();
            
            _done = false;
            
            SetColumnNumbers();
            
            if (_elements.Count == 0)
            {
                foreach (var column in columns)
                {
                    AddElementsToList(column.GetElements());
                }
            }
            
            PlayShakeAnimation();
        }

        private void SetColumnNumbers()
        {
            int start = 1;
            int range = 15;

            for (int i = 0; i < columns.Count; i++)
            {
                int from = start + (i * range);
                int to   = from + range - 1;

                columns[i].Init(_bingoFieldId, from, to);
            }
        }

        private void AddElementsToList(List<BingoFieldElement> columnElements)
        {
            foreach (var element in columnElements)
            {
                _elements.Add(element);
            }
        }

        public void AddRandomNumber()
        {
            List<int> availableElements = new List<int>();

            for (int i = 0; i < _elements.Count; i++)
            {
                if (!_elements[i].Done)
                {
                    availableElements.Add(i);
                }
            }

            if (availableElements.Count == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, availableElements.Count);
            _elements[availableElements[randomIndex]].SetAsDone();
            
            CheckBingoCombinations();
        }

        public bool HasUndoneElements()
        {
            if (_done)
                return false;

            foreach (var element in _elements)
            {
                if (!element.Done)
                    return true;
            }

            return false;
        }

        private void CheckBingoCombinations()
        {
            if (!_active) return;
            
            var currentFieldCombination = GetCurrentFieldCombination();
            var currentPlayerCombinations = _bingoLogic.TryFindMatchingCombination(currentFieldCombination);
            
            if (currentPlayerCombinations.Count <= 0)
                return;

            foreach (var currentPlayerCombination in currentPlayerCombinations)
                ShowWinCombinationButtons(currentPlayerCombination);
        }

        private Combination GetCurrentFieldCombination()
        {
            _currentCombination.Reset();

            for (int i = 0; i < columns.Count; i++) 
            {
                for (int j = 0 ; j < columns[i].GetElements().Count; j++) 
                {
                    bool done = columns[i].GetElements()[j].Done;
                    _currentCombination.Set(i, j, done);
                }
            }

            return _currentCombination;
        }
        
        private void ShowWinCombinationButtons(Combination bingoCombination)
        {
            for (int i = 0; i < columns.Count; i++) 
            {
                for (int j = 0 ; j < columns[i].GetElements().Count; j++) 
                {
                    if (bingoCombination.Get(i,j))
                    {
                        columns[i].GetElements()[j].SetAsCombinationState();
                    }
                }
            }
        }

        private void SetAsDone(BingoFieldElement.CompleteFieldButtonClickedSignal completeFieldButtonClickedSignal)
        {
            if (_bingoFieldId != completeFieldButtonClickedSignal.BingoFieldId) return;
            
            _done = true;
            
            activeState.Exit();
            doneState.Enter(() =>
            {
                doneState.Exit(() =>
                {
                    _signalBus.Fire(new CompletedSignal()); 
                });
            });
            
            PlayShakeAnimation();
        }
        
        private void PlayShakeAnimation()
        {
            contentTransform.DOShakeScale(.2f, .2f);
        }
        
        public class CompletedSignal 
        {
        }
    }
}
