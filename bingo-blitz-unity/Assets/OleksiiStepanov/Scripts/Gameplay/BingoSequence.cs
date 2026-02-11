using System;
using System.Collections.Generic;
using BingoBlitzClone.Utils;
using Zenject;

namespace BingoBlitzClone.Gameplay
{
    public class BingoSequence: IInitializable, IDisposable
    {
        private List<int> _bingoSequence = new List<int>();
        private readonly List<int> _activeSequence = new List<int>();

        private CountdownTimer _countdownTimer;
        
        private GameRules _gameRules;
        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus, GameRules gameRules)
        {
            _gameRules = gameRules;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _bingoSequence = ListTools.GetRandomizedList(1,75);

            _countdownTimer = new CountdownTimer(_gameRules.BingoBallAppearTime);
            _countdownTimer.OnTimerEnd += ShowBingoBall;
            
            _signalBus.Subscribe<PauseSignal>(Pause);
        }

        public void Dispose()
        {
            _countdownTimer.OnTimerEnd -= ShowBingoBall;
            _signalBus.Unsubscribe<PauseSignal>(Pause);
        }

        private void Pause(PauseSignal signal)
        {
            if (signal.Paused)
            {
                _countdownTimer.Stop();
            }
            else
            {
                _countdownTimer.Continue();
            }
        }

        public void StartBingoSequence()
        {
            ShowBingoBall();
        }

        public void Stop()
        {
            _countdownTimer.Stop();
        }

        private void ShowBingoBall()
        {
            if (_bingoSequence.Count <= 0)
            {
                _signalBus.Fire<CompletedSignal>();
                return;
            }
            
            var number = _bingoSequence[0];
            _bingoSequence.RemoveAt(0);
            
            _countdownTimer.Reset();
            _countdownTimer.Start();
            
            if (_activeSequence.Count < 7)
            {
                _activeSequence.Add(number);    
            }
            else
            {
                _activeSequence.RemoveAt(0);
                _activeSequence.Add(number);
            }
            
            _signalBus.Fire(new NewNumberSignal(number));
        }

        public bool IsNumberInActiveSequence(int number)
        {
            return _activeSequence.Contains(number);
        }
        
        public class NewNumberSignal
        {
            public int Number { get; }
            public NewNumberSignal(int number)
            {
                Number = number;
            }
        }
        
        public class CompletedSignal
        {
        }
    }   
}