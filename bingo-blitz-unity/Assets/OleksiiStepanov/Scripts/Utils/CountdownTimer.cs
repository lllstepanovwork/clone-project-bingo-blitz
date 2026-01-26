using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BingoBlitzClone.Utils
{
    public class CountdownTimer
    {
        private float TimeRemaining { get; set; }
        private bool TimerIsRunning { get; set; }
    
        public event Action OnTimerEnd; 
    
        public CountdownTimer(float initialTime)
        {
            TimeRemaining = initialTime;
            TimerIsRunning = false;
        }
    
        public void Start()
        {
            TimerIsRunning = true;
            RunTimerAsync().Forget();
        }
    
        private async UniTaskVoid RunTimerAsync()
        {
            while (TimerIsRunning && TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                await UniTask.Yield();
            }
    
            if (TimeRemaining <= 0)
            {
                TimerIsRunning = false;
                TimeRemaining = 0;
                OnTimerEnd?.Invoke();
            }
        }
    
        public void Stop()
        {
            TimerIsRunning = false;
        }
    
        public void Reset(float newTime)
        {
            TimeRemaining = newTime;
            TimerIsRunning = false;
        }
    }
}

