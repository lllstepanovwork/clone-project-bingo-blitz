using System;
using System.Collections.Generic;
using OleksiiStepanov.Game;
using OleksiiStepanov.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace OleksiiStepanov.Gameplay
{
    public class BingoSequence : MonoBehaviour
    {
        [SerializeField] private List<BingoBall> bingoBalls = new List<BingoBall>();
        [SerializeField] private Transform layout;
        
        [SerializeField] private List<int> bingoSequence = new List<int>();
        private readonly CountdownTimer _countdownTimer = new CountdownTimer(Constants.BINGO_SEQUENCE_FIRST_TIME);

        [SerializeField] private List<Sprite> bingoBallsSprites = new List<Sprite>();

        public static event Action OnSequenceFinished;
        public static event Action<int> OnNewBingoNumberCreated;
        
        private void OnEnable()
        {
            _countdownTimer.OnTimerEnd += OnTimerEnd;
        }

        private void OnDisable()
        {
            _countdownTimer.OnTimerEnd -= OnTimerEnd;
        }

        public void Init()
        {
            bingoSequence = ListTools.GetRandomizedList(1,75);

            for (int i = 0; i < bingoBalls.Count; i++)
            {
                bingoBalls[i].Init();
            }
        }

        public void StartBingoSequence()
        {
            _countdownTimer.Start();
        }

        public void Stop()
        {
            _countdownTimer.Stop();
        }

        private void OnTimerEnd()
        {
            if (bingoSequence.Count <= 0)
            {
                OnSequenceFinished?.Invoke();
                return;
            }

            ListTools.MoveLastToFirst(bingoBalls);
            
            var number = bingoSequence[0];
            bingoSequence.RemoveAt(0);

            Sprite bingoBallSprite = GetBingoBallSprite(number);
            
            bingoBalls[0].Show(number, bingoBallSprite);
            bingoBalls[0].transform.SetAsFirstSibling();
            
            _countdownTimer.Reset(Constants.BINGO_SEQUENCE_REPEAT_TIME);
            _countdownTimer.Start();
            
            UpdateBingoBallsVisuals();
            
            OnNewBingoNumberCreated?.Invoke(number);
        }

        private Sprite GetBingoBallSprite(int number)
        {
            var index = (number - 1) / 15;
            return bingoBallsSprites[index];
        }

        private void UpdateBingoBallsVisuals()
        {
            bingoBalls[1].ScaleDown();
            bingoBalls[^3].Hide(BingoSequenceTransparencyType.Quarter);
            bingoBalls[^2].Hide(BingoSequenceTransparencyType.Half);
            bingoBalls[^1].Hide(BingoSequenceTransparencyType.Clear);
        }
    }   
}
