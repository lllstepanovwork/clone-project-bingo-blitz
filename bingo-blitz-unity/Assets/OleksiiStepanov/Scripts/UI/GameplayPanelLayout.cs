using System;
using System.Collections.Generic;
using OleksiiStepanov.Gameplay;
using UnityEngine;

namespace OleksiiStepanov.UI
{
    public class GameplayPanelLayout : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private BingoSequence bingoSequence;
        [SerializeField] private List<BingoField> bingoFields;

        private int _bingoFieldDoneCounter = 0;
        
        public static event Action OnGameOver; 
        
        public void Init()
        {
            _bingoFieldDoneCounter = 0;
            
            foreach (var bingoField in bingoFields)
            {
                bingoField.Init();
            }
            
            bingoSequence.Init();
        }
        
        public void StartGame() 
        {
            bingoSequence.StartBingoSequence();
        }
        
        public void StopGame() 
        {
            bingoSequence.Stop();
        }

        public void PlayShakeAnimation()
        {
            foreach (var bingoField in bingoFields)
            {
                bingoField.PlayShakeAnimation();
            }
        }

        private void OnEnable()
        {
            BingoField.OnBingoFieldCompleted += BingoFieldOnOnBingoFieldCompleted;
        }
        
        private void OnDisable()
        {
            BingoField.OnBingoFieldCompleted -= BingoFieldOnOnBingoFieldCompleted;
        }
        
        private void BingoFieldOnOnBingoFieldCompleted()
        {
            _bingoFieldDoneCounter++;

            if (_bingoFieldDoneCounter != bingoFields.Count) return;
            
            bingoSequence.Stop();
            OnGameOver?.Invoke();
        }
    }
}
