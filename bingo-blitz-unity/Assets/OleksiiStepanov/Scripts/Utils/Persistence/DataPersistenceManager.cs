using System;
using UnityEngine;

namespace OleksiiStepanov.Utils.Persistence
{
    public class DataPersistenceManager : MonoBehaviour
    {
        public GameData gameData;

        private IDataService dataService;

        protected void Awake()
        {
            dataService = new FileDataService(new JsonSerializer());
        }

        public void CreateNewGame()
        {
            gameData = new GameData {
                Name = "New Game"
            };
        }

        public void SaveGame()
        {
            dataService.Save(gameData);
        }

        public void LoadGame(string saveName)
        {
            gameData = dataService.Load(saveName);
        }

        public void DeleteSave(string saveName)
        {
            dataService.Delete(saveName);
        }

        public void DeleteAll() => dataService.DeleteAll();
    }

    [Serializable]
    public class GameData
    {
        public string Name;
    }
}

