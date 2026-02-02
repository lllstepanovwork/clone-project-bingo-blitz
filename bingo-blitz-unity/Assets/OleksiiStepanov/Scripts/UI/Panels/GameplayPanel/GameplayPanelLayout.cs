using System;
using System.Collections.Generic;
using BingoBlitzClone.Gameplay;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.UI
{
    public class GameplayPanelLayout : MonoBehaviour
    {
        [Header("Content")]
        [SerializeField] private BingoSequenceViewer bingoSequenceViewer;
        [SerializeField] private List<BingoField> bingoFields;

        private int _bingoFieldDoneCounter = 0;
        
        public static event Action OnWin; 
        
        private BingoSequence _bingoSequence;
        
        [Inject]
        public void Construct(BingoSequence bingoSequence)
        {
            _bingoSequence = bingoSequence;
        }
        
        private void OnEnable()
        {
            BingoField.OnBingoFieldCompleted += BingoFieldOnOnBingoFieldCompleted;
        }
        
        private void OnDisable()
        {
            BingoField.OnBingoFieldCompleted -= BingoFieldOnOnBingoFieldCompleted;
        }
        
        public void Init()
        {
            _bingoFieldDoneCounter = 0;
            
            for (var i = 0; i < bingoFields.Count; i++)
            {
                bingoFields[i].Init();
            }
            
            bingoSequenceViewer.Init();
        }

        public void StartGame() 
        {
            _bingoSequence.StartBingoSequence();
        }
        
        public void StopGame() 
        {
            _bingoSequence.Stop();
        }

        public void PlayShakeAnimation()
        {
            foreach (var bingoField in bingoFields)
            {
                bingoField.PlayShakeAnimation();
            }
        }
        
        private void BingoFieldOnOnBingoFieldCompleted()
        {
            _bingoFieldDoneCounter++;

            if (_bingoFieldDoneCounter != bingoFields.Count) return;
            
            _bingoSequence.Stop();
            OnWin?.Invoke();
        }
    }
}
