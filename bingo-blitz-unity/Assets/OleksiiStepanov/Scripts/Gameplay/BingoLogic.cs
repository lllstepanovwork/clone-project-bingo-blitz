using System;
using System.Collections.Generic;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    public class BingoLogic
    {
        private CombinationInfo _combinationInfo;
        private SignalBus _signalBus;
        private BingoSequence _bingoSequence;

        private int _layoutNumber;

        private readonly List<Combination> _currentPlayerCombinations = new List<Combination>();
        
        [Inject]
        public void Construct(SignalBus signalBus, CombinationInfo combinationInfo,  BingoSequence bingoSequence)
        {
            _combinationInfo = combinationInfo;
            _signalBus = signalBus;
            _bingoSequence = bingoSequence;
        }
        
        public void AddNumberToCurrentCombination(int number, Action onComplete)
        {
            bool success = _bingoSequence.IsNumberInActiveSequence(number);

            if (!success) return;
            
            onComplete?.Invoke();
            
            _signalBus.Fire(new NumberMatchSignal());  
        }

        public List<Combination> TryFindMatchingCombination(Combination fieldCombination)
        {   
            _currentPlayerCombinations.Clear();
            
            foreach (var combination in _combinationInfo.Combinations) 
            {
                if (combination.IsMatch(fieldCombination))
                    _currentPlayerCombinations.Add(combination);
            }
            
            return _currentPlayerCombinations;
        }
        
        public class NumberMatchSignal
        {
        }
    }
}