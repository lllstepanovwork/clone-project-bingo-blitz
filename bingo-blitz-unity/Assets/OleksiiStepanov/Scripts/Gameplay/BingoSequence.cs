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
        
        public static event Action OnSequenceFinished;
        public static event Action<int> OnNewBingoNumberCreated;
        
        private GameRules _gameRules;
        
        [Inject]
        public void Construct(GameRules gameRules)
        {
            _gameRules = gameRules;
        }
        
        public void Initialize()
        {
            _bingoSequence = ListTools.GetRandomizedList(1,75);

            _countdownTimer = new CountdownTimer(_gameRules.BingoBallAppearTime);
            _countdownTimer.OnTimerEnd += ShowBingoBall;
        }

        public void Dispose()
        {
            _countdownTimer.OnTimerEnd -= ShowBingoBall;
        }

        public void StartBingoSequence()
        {
            ShowBingoBall();
            _countdownTimer.Start();
        }

        public void Stop()
        {
            _countdownTimer.Stop();
        }

        private void ShowBingoBall()
        {
            if (_bingoSequence.Count <= 0)
            {
                OnSequenceFinished?.Invoke();
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
            
            OnNewBingoNumberCreated?.Invoke(number);
        }

        public bool IsNumberInActiveSequence(int number)
        {
            return _activeSequence.Contains(number);
        }
    }   
}