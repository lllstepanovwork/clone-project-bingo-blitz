using BingoBlitzClone.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace BingoBlitzClone.Loading
{
    public class AppLoader : IInitializable
    {
        private AsyncOperation _currentOperation;

        public void Initialize()
        {
            StartLoading();
        }

        private void StartLoading()
        {
            Debug.Log("Start Loading");

            if (SceneManager.GetSceneByName(Constants.MAIN_SCENE_NAME).isLoaded)
                return;

            if (_currentOperation != null && !_currentOperation.isDone)
                return;

            _currentOperation =
                SceneManager.LoadSceneAsync(Constants.MAIN_SCENE_NAME, LoadSceneMode.Additive);

            if (_currentOperation != null) 
                _currentOperation.completed += _ => _currentOperation = null;
        }
    }
}