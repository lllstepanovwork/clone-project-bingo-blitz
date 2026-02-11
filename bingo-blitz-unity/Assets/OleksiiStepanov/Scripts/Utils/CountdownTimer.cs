using System;
using BingoBlitzClone.Gameplay;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Utils
{
    public class CountdownTimer 
    {
        private float TotalTime { get; set; }
        private float TimeRemaining { get; set; }
        private bool IsRunning { get; set; }
    
        public event Action OnTimerEnd;

        public CountdownTimer(float initialTime)
        {
            TotalTime = initialTime;
            IsRunning = false;
        }
    
        public void Start()
        {
            TimeRemaining = TotalTime;
            IsRunning = true;
            
            RunTimerAsync().Forget();
        }
    
        private async UniTaskVoid RunTimerAsync()
        {
            while (IsRunning && TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                await UniTask.Yield();
            }
    
            if (TimeRemaining <= 0)
            {
                IsRunning = false;
                TimeRemaining = 0;
                OnTimerEnd?.Invoke();
            }
        }
    
        public void Continue()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                RunTimerAsync().Forget();
            }
        }
        
        public void Stop()
        {
            IsRunning = false;
        }
    
        public void Reset()
        {
            Stop();
            IsRunning = false;
            TimeRemaining = TotalTime;
        }
    }
}

