using System;
using System.Collections.Generic;
using BingoBlitzClone.Game;
using UnityEngine;
using Zenject;

namespace BingoBlitzClone.Loading
{
    public class GameLoader : IInitializable
    {
        public bool LoadingComplete = false;

        public event Action<LoadingStep> LoadingStepCompleted = null;
        public event Action LoadingCompleted = null;

        private readonly LoadingStepFactory _factory;

        private List<LoadingStepBase> _loadingSteps;
        private LoadingStepBase CurrentLoadingStep { get; set; }
        
        private int _currentLoadingStepIndex = 0;

        [Inject]
        public GameLoader(LoadingStepFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            StartLoading();
        }

        private void StartLoading()
        {
            LoadingComplete = false;

            _loadingSteps = new List<LoadingStepBase>
            {
                _factory.Create<LoadingStepAppInit>(),
                _factory.Create<LoadingStepUIInit>(),
                _factory.Create<LoadingStepComplete>()
            };

            foreach (var step in _loadingSteps)
            {
                step.Exited += GoToNextStep;
            }

            SetCurrentLoadingStep(_loadingSteps[0]);
        }

        private void GoToNextStep()
        {
            if (CurrentLoadingStep != null)
            {
                LoadingStepCompleted?.Invoke(CurrentLoadingStep.GetStepType());
            }

            _currentLoadingStepIndex++;

            if (_currentLoadingStepIndex < _loadingSteps.Count)
            {
                SetCurrentLoadingStep(_loadingSteps[_currentLoadingStepIndex]);
            }
            else
            {
                LoadingComplete = true;
                LoadingCompleted?.Invoke();
            }
        }

        private void SetCurrentLoadingStep(LoadingStepBase step)
        {
            Debug.Log($"Entering: {step} step!");
            CurrentLoadingStep = step;
            CurrentLoadingStep.Enter();
        }
    }
}