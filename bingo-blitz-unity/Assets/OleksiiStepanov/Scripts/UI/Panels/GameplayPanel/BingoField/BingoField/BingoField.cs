using System;
using System.Collections.Generic;
using BingoBlitzClone.UI;
using BingoBlitzClone.Utils;
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
        [SerializeField] private List<BingoFieldElement> columnB = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnI = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnN = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnG = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnO = new List<BingoFieldElement>();
        
        private readonly List<BingoFieldElement> _elements = new List<BingoFieldElement>();
        
        public static event Action OnMatch;
        public static event Action OnBingoFieldCompleted;
        
        private BingoLogic _bingoLogic;
        private BingoSequence _bingoSequence;
        private BingoCombinations _combinations;

        private int _fieldNumber;
        
        [Inject]
        public void Construct(BingoLogic bingoLogic, BingoSequence bingoSequence, BingoCombinations combinations)
        {
            _bingoLogic = bingoLogic;
            _bingoSequence = bingoSequence;
            _combinations = combinations;
        }
        
        private void OnEnable()
        {
            ComboCounter.OnReward += OnComboCounterReward;
        }

        private void OnDisable()
        {
            ComboCounter.OnReward -= OnComboCounterReward;
        }

        public void Init()
        {
            activeState.Enter();

            InitColumn(columnB, 1, 15);
            InitColumn(columnI, 16, 30);
            InitColumn(columnN, 31, 45);
            InitColumn(columnG, 46, 60);
            InitColumn(columnO, 61, 75);

            if (_elements.Count == 0)
            {
                AddElementsToList(columnB);
                AddElementsToList(columnI);
                AddElementsToList(columnN);
                AddElementsToList(columnG);
                AddElementsToList(columnO);   
            }
            
            PlayShakeAnimation();
        }

        private void InitColumn(List<BingoFieldElement> columnElements, int minValue, int maxValue)
        {
            List<int> randomValueList = ListTools.GetRandomizedList(minValue, maxValue);

            int count = Math.Min(columnElements.Count, randomValueList.Count);
            for (int i = 0; i < count; i++)
            {
                columnElements[i].Init(this, randomValueList[i]);
            }
        }

        private void AddElementsToList(List<BingoFieldElement> columnElements)
        {
            foreach (var element in columnElements)
            {
                _elements.Add(element);
            }
        }

        private void OnComboCounterReward()
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

        public void OnBingoFieldElementButtonClick(BingoFieldElement bingoFieldElement)
        {
            if (!_bingoSequence.IsNumberInActiveSequence(bingoFieldElement.Number))
                return;

            bingoFieldElement.SetAsDone();
            
            OnMatch?.Invoke();
            
            CheckBingoCombinations();
        }

        private void CheckBingoCombinations()
        {   
            foreach (var bingoCombination in _combinations.Combinations)
            {
                int counter = 0;
                
                for (int i = 0; i < bingoCombination.Length; i++)
                {
                    if (_elements[bingoCombination[i]].Done)
                    {
                        counter++;
                    }
                }

                if (counter == bingoCombination.Length)
                {
                    ShowWinCombinationButtons(bingoCombination);
                }
            }
        }

        private void ShowWinCombinationButtons(int[] bingoCombination)
        {
            for (int i = 0; i < bingoCombination.Length; i++)
            {
                _elements[bingoCombination[i]].SetAsCombinationState();
            }
        }

        public void SetAsDone()
        {
            activeState.Exit();
            doneState.Enter(() =>
            {
                doneState.Exit(() =>
                {
                    Debug.Log("BingoField is done");
                    OnBingoFieldCompleted?.Invoke();   
                });
            });
            
            PlayShakeAnimation();
        }
        
        private void PlayShakeAnimation()
        {
            contentTransform.DOShakeScale(.2f, .2f);
        }
    }
}
