using System;
using System.Collections.Generic;
using DG.Tweening;
using OleksiiStepanov.UI;
using OleksiiStepanov.Utils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        
        [Header("Done State Animation")]
        [SerializeField] private Image doneStateBackground;
        [SerializeField] private List<RectTransform> bingoLetters = new List<RectTransform>();
        
        public static event Action OnMatch;
        public static event Action OnBingoFieldCompleted;
        
        private List<int[]> _bingoCombinations = new List<int[]>();
        
        private readonly BingoCombinations _combinations = new BingoCombinations(5);
        private readonly List<int> _currentBingoNumbers = new List<int>();
        
        public void Init()
        {
            _bingoCombinations = _combinations.GetBingoCombinations();
            
            activeState.SetActive(true);
            doneState.SetActive(false);

            for (int i = 0; i < bingoLetters.Count; i++)
            {
                bingoLetters[i].localScale = Vector3.zero;
            }

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
                columnElements[i].Init(this, randomValueList[i]);
            }
        }

        private void OnEnable()
        {
            BingoSequence.OnNewBingoNumberCreated += OnNewBingoNumberCreated; 
            ComboCounter.OnReward += OnComboCounterReward;
        }

        private void OnDisable()
        {
            BingoSequence.OnNewBingoNumberCreated -= OnNewBingoNumberCreated;
            ComboCounter.OnReward -= OnComboCounterReward;
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
        
        private void OnComboCounterReward()
        {
            while (true)
            {
                int randomIndex = Random.Range(0, _currentBingoNumbers.Count);

                if (!elements[randomIndex].Done)
                {
                    elements[randomIndex].SetAsDone();
                    break;
                }
            }
        }

        public void OnBingoFieldElementButtonClick(BingoFieldElement bingoFieldElement)
        {
            foreach (var bingoNumber in _currentBingoNumbers)
            {
                if (bingoFieldElement.Number != bingoNumber) continue;
                
                bingoFieldElement.SetAsDone();
                
                OnMatch?.Invoke();
                
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
            
            PlayShakeAnimation();
            PlayDoneStateAnimation(() =>
            {
                OnBingoFieldCompleted?.Invoke();    
            });
        }

        public void PlayShakeAnimation()
        {
            contentTransform.DOShakeScale(.2f, .2f);
        }

        private void PlayDoneStateAnimation(Action onAnimationComplete = null)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(doneStateBackground.DOFade(1, 0.1f));

            foreach (var letter in bingoLetters)
            {
                sequence.Append(letter.DOScale(1.5f, 0.065f));
                sequence.Append(letter.DOScale(1f, 0.065f));
            }

            sequence.AppendCallback(() =>
            {
                onAnimationComplete?.Invoke();
            });
        }
    }
}
