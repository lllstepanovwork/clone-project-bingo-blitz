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
        private SignalBus _signalBus;
        
        private int _currentComboCounter = 0;
        
        private ComboCounterStateType _currentComboCounterState;

        [Inject]
        public void Construct(GameRules gameRules, SignalBus signalBus)
        {
            _gameRules = gameRules;
            _signalBus = signalBus;
        }
        
        private void OnEnable()
        {
            _signalBus.Subscribe<BingoLogic.NumberMatchSignal>(OnMatch);
        }
        
        private void OnDisable()
        {
            _signalBus.Unsubscribe<BingoLogic.NumberMatchSignal>(OnMatch);
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
            
            _signalBus.Fire(new RewardSignal());         
        }

        private enum ComboCounterStateType
        {
            CounterState,
            RewardState,
            CooldownState
        }
    }    
    
    public class RewardSignal
    {
    }
}

