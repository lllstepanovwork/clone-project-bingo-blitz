using OleksiiStepanov.Game;
using OleksiiStepanov.Utils;
using UnityEngine;

using UnityEngine.SceneManagement;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace OleksiiStepanov.App
{
    public class AppLoader : SingletonBehaviour<AppLoader>
    {
        public string Platform;
        public string Version;
        public string BuildNumber;

        public GameObject LoaderCanvas = null;

        private bool loadingMainScene = false;

        public void Start()
        {
            SetAppDetails();

            LoaderCanvas.SetActive(true);

            if (!SceneManager.GetSceneByName(Constants.MAIN_SCENE_NAME).isLoaded)
            {
                SceneManager.LoadSceneAsync(Constants.MAIN_SCENE_NAME, LoadSceneMode.Additive).completed += (obj) =>
                {
                    // Main scene was loaded
                    loadingMainScene = false;
                };
            }
            else
            {
                loadingMainScene = false;
            }
        }

        private void SetAppDetails()
        {
            Platform = Application.platform.ToString();
            Version = Application.version;
#if UNITY_EDITOR
#if UNITY_ANDROID
//        BuildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
#elif UNITY_IOS
//        BuildNumber = PlayerSettings.iOS.buildNumber;
#endif
#endif
        }

        public void ReloadMainScene()
        {
            // Check if we are loading/unloading the Main scene
            if (loadingMainScene)
            {
                return;
            }

            LoaderCanvas.SetActive(true);

            // Mark Main scene as loading
            loadingMainScene = true;
            SceneManager.UnloadSceneAsync(Constants.MAIN_SCENE_NAME).completed += (obj1) =>
            {
                SceneManager.LoadSceneAsync(Constants.MAIN_SCENE_NAME, LoadSceneMode.Additive).completed += (obj2) =>
                {
                    // Main scene has been loaded
                    loadingMainScene = false;
                };
            };
        }
    }
}