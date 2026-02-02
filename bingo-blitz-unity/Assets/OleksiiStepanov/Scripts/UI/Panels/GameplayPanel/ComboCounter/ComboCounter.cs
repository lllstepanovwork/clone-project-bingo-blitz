using UnityEngine;
using System;
using BingoBlitzClone.Gameplay;
using Zenject;

namespace BingoBlitzClone.UI
{
    public class ComboCounter : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private ComboCounterCountState countState;
        [SerializeField] private ComboCounterRewardState rewardState;
        [SerializeField] private ComboCounterCooldownState cooldownState;
        
        private GameRules _gameRules;
        
        private int _currentComboCounter = 0;
        
        public static event Action OnReward;
        
        private ComboCounterStateType _currentComboCounterState;

        [Inject]
        public void Construct(GameRules gameRules)
        {
            _gameRules = gameRules;
        }
        
        private void OnEnable()
        {
            BingoField.OnMatch += OnMatch;
        }
        
        private void OnDisable()
        {
            BingoField.OnMatch -= OnMatch;
        }
        
        public void Init()
        {
            ShowState(ComboCounterStateType.CounterState);
        }
        
        private void ShowState(ComboCounterStateType state)
        {
            HideAllStates();
            
            _currentComboCounterState = state;

            switch (state)
            {
                case ComboCounterStateType.CounterState:
                    _currentComboCounter = 0;
                    countState.Init();
                    break;
                case ComboCounterStateType.RewardState:
                    rewardState.Init();
                    break;
                case ComboCounterStateType.CooldownState:
                    cooldownState.Init(() =>
                    {
                        ShowState(ComboCounterStateType.CounterState);
                    });
                    break;
            }
        }

        private void OnMatch()
        {
            if (_currentComboCounterState != ComboCounterStateType.CounterState)
            {
                return;
            }

            _currentComboCounter++;
            
            countState.UpdateFill(_currentComboCounter, () =>
            {
                if (_currentComboCounter == _gameRules.MaxCombo)
                {
                    _currentComboCounter = 0;

                    ShowState(ComboCounterStateType.RewardState);
                }
            });
        }

        private void HideAllStates()
        {
            rewardState.ResetState();
            cooldownState.ResetState();
            countState.ResetState();
        }

        public void OnRewardButtonClicked()
        {
            rewardState.PlayRewardStateClickAnimation(()=>
            {
                ShowState(ComboCounterStateType.CooldownState);    
            });
            
            OnReward?.Invoke();            
        }

        private enum ComboCounterStateType
        {
            CounterState,
            RewardState,
            CooldownState
        }

    }    
}

