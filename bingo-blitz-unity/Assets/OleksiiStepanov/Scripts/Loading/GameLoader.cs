using System;
using System.Collections.Generic;
using OleksiiStepanov.Game;
using OleksiiStepanov.Utils;
using UnityEngine;

namespace OleksiiStepanov.Loading
{
    public class GameLoader : SingletonBehaviour<GameLoader>
    {
        [NonSerialized] public List<LoadingStepBase> LoadingSteps = null;

        public LoadingStepBase CurrentLoadingStep { get; private set; }

        public event Action<LoadingStep> LoadingStepCompleted = null;

        public event Action LoadingCompleted = null;

        [HideInInspector] public bool LoadingComplete = false;

        private int currentLoadingStepIndex = 0;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            LoadingComplete = false;
            LoadingSteps = new List<LoadingStepBase>
            {
                new LoadingStep_AppInit(),
                new LoadingStep_UIInit(),
                new LoadingStep_Complete()
            };

            foreach (LoadingStepBase step in LoadingSteps)
            {
                step.Exited += GoToNextStep;
            }

            if (LoadingSteps != null && LoadingSteps.Count > 0)
            {
                SetCurrentLoadingStep(LoadingSteps[0]);
            }
            else
            {
                Debug.LogError("Loading steps list is null or empty");
                Application.Quit();
            }
        }

        public void GoToNextStep()
        {
            if (CurrentLoadingStep != null)
            {
                LoadingStepCompleted?.Invoke(CurrentLoadingStep.GetStepType());
            }

            currentLoadingStepIndex++;

            if (currentLoadingStepIndex < LoadingSteps.Count)
            {
                SetCurrentLoadingStep(LoadingSteps[currentLoadingStepIndex]);
            }
            else
            {
                LoadingComplete = true;
                LoadingCompleted?.Invoke();
            }
        }

        private void SetCurrentLoadingStep(LoadingStepBase step)
        {
            Debug.Log($"{Constants.CONSOLE_MESSAGE_COLOR_BLUE}Entering: {step} step!{Constants.CONSOLE_MESSAGE_COLOR_END}");
            CurrentLoadingStep = step;

            CurrentLoadingStep.Enter();
        }
    }
}