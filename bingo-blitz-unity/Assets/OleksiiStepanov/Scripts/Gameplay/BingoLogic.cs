using System;
using System.Collections.Generic;
using BingoBlitzClone.UI;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    public class BingoLogic : IInitializable, IDisposable
    {
        private BingoSequence _bingoSequence;
        private SignalBus _signalBus;

        private int _layoutNumber;
        private readonly List<Combination> _combinations = new List<Combination>();
        
        [Inject]
        public void Construct(SignalBus signalBus, BingoSequence bingoSequence)
        {
            _signalBus = signalBus;
            _bingoSequence = bingoSequence;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<LayoutSelectedSignal>(OnLayoutSelected);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<LayoutSelectedSignal>(OnLayoutSelected);
        }

        private void OnLayoutSelected(LayoutSelectedSignal signal)
        {
            _layoutNumber = signal.LayoutNumber;

            for (int i = 0; i < _layoutNumber; i++) 
            {
                if (_combinations.Count > 0 && _combinations[i] != null)
                {
                    _combinations[i].Reset();
                    continue;
                }

                var combination = new Combination();
                _combinations.Add(combination);
            }
        }
    }
}