using System;
using System.Collections.Generic;
using BingoBlitzClone.UI;
using BingoBlitzClone.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BingoBlitzClone.Gameplay
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
        
        private List<int> _currentBingoNumbers = new List<int>();
        
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

        private void OnNewBingoNumberCreated(List<int> activeSequence)
        {
            _currentBingoNumbers = activeSequence;
        }
        
        private void OnComboCounterReward()
        {
            List<int> availableElements = new List<int>();

            for (int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].Done)
                {
                    availableElements.Add(i);
                }
            }

            if (availableElements.Count == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, availableElements.Count);
            elements[availableElements[randomIndex]].SetAsDone();
            
            CheckBingoCombinations();
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
                elements[bingoCombination[i]].SetAsCombinationState();
            }
        }

        public void SetAsDone()
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
