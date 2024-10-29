using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using OleksiiStepanov.Utils;

namespace OleksiiStepanov.Gameplay
{
    public class BingoField : MonoBehaviour
    {
        [Header("Content")] 
        [SerializeField] private Transform contentTransform;
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject doneState;
        [SerializeField] private List<BingoFieldElement> elements = new List<BingoFieldElement>();

        [Header("Columns")]
        [SerializeField] private List<BingoFieldElement> columnB = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnI = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnG = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnN = new List<BingoFieldElement>();
        [SerializeField] private List<BingoFieldElement> columnO = new List<BingoFieldElement>();
        
        public static event Action OnBingoFieldCompleted;
        
        private List<int[]> _bingoCombinations = new List<int[]>();
        
        private readonly BingoCombinations _combinations = new BingoCombinations(5);
        private readonly List<int> _currentBingoNumbers = new List<int>();
        
        public void Init()
        {
            _bingoCombinations = _combinations.GetBingoCombinations();    
            
            InitColumn(columnB, 1, 15);
            InitColumn(columnI, 16, 30);
            InitColumn(columnG, 31, 45);
            InitColumn(columnN, 46, 60);
            InitColumn(columnO, 61, 75);
        }

        private void InitColumn(List<BingoFieldElement> columnElements, int minValue, int maxValue)
        {
            List<int> randomValueList = ListTools.GetRandomizedList(minValue, maxValue);

            int count = Math.Min(columnElements.Count, randomValueList.Count);
            for (int i = 0; i < count; i++)
            {
                columnElements[i].Init(randomValueList[i]);
            }
        }

        private void OnEnable()
        {
            BingoSequence.OnNewBingoNumberCreated += OnNewBingoNumberCreated; 
            BingoFieldElement.OnButtonClick += OnBingoFieldElementButtonClick;
        }

        private void OnDisable()
        {
            BingoSequence.OnNewBingoNumberCreated -= OnNewBingoNumberCreated;
            BingoFieldElement.OnButtonClick -= OnBingoFieldElementButtonClick;
        }

        private void OnNewBingoNumberCreated(int number)
        {
            if (_currentBingoNumbers.Count < 7)
            {
                _currentBingoNumbers.Add(number);    
            }
            else
            {
                _currentBingoNumbers.RemoveAt(_currentBingoNumbers.Count - 1);
                _currentBingoNumbers.Insert(0, number);
            }
        }

        private void OnBingoFieldElementButtonClick(BingoFieldElement bingoFieldElement)
        {
            foreach (var bingoNumber in _currentBingoNumbers)
            {
                if (bingoFieldElement.Number != bingoNumber) continue;
                
                bingoFieldElement.SetAsDone();
                break;
            }
            
            CheckBingoCombinations();
        }

        private void CheckBingoCombinations()
        {
            foreach (var bingoCombination in _bingoCombinations)
            {
                int counter = 0;
                
                for (int i = 0; i < bingoCombination.Length; i++)
                {
                    if (elements[bingoCombination[i]].Done)
                    {
                        counter++;
                    }
                }

                if (counter != bingoCombination.Length) continue;
                
                SetAsDone();
                break;
            }
        }

        private void SetAsDone()
        {
            activeState.SetActive(false);
            doneState.SetActive(true);
            
            OnBingoFieldCompleted?.Invoke();
        }

        public void PlayShakeAnimation()
        {
            contentTransform.DOShakeScale(.2f, .2f);
        }
    }
}
