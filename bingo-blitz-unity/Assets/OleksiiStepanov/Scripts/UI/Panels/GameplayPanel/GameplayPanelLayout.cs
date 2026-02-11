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
        
        private BingoSequence _bingoSequence;
        private SignalBus _signalBus;

        private bool _active;
        
        [Inject]
        public void Construct(SignalBus signalBus, BingoSequence bingoSequence)
        {
            _signalBus = signalBus;
            _bingoSequence = bingoSequence;
        }
        
        private void OnEnable()
        {
            _active = true;
            
            _signalBus.Subscribe<BingoField.CompletedSignal>(OnBingoFieldCompleted);
            _signalBus.Subscribe<RewardSignal>(AddRandomNumber);
        }
        
        private void OnDisable()
        {
            _active = false;
            
            _signalBus.Unsubscribe<BingoField.CompletedSignal>(OnBingoFieldCompleted);
            _signalBus.Unsubscribe<RewardSignal>(AddRandomNumber);
        }
        
        public void Init()
        {
            _bingoFieldDoneCounter = 0;
            
            for (var i = 0; i < bingoFields.Count; i++)
            {
                bingoFields[i].Init(i);
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
        
        private void OnBingoFieldCompleted()
        {
            _bingoFieldDoneCounter++;

            if (_bingoFieldDoneCounter != bingoFields.Count) return;
            
            _bingoSequence.Stop();
            
            _signalBus.Fire(new CompletedSignal());
        }

        private void AddRandomNumber()
        {
            if (!_active) return;
            
            List<BingoField> availableFields = new List<BingoField>();

            foreach (var bingoField in bingoFields)
            {
                if (bingoField.HasUndoneElements())
                    availableFields.Add(bingoField);
            }

            if (availableFields.Count == 0)
            {
                Debug.LogWarning("No bingoFields found");
                return;
            }

            int randomIndex = Random.Range(0, availableFields.Count);
            availableFields[randomIndex].AddRandomNumber();
        }

        public class CompletedSignal
        {
        }
    }
}
