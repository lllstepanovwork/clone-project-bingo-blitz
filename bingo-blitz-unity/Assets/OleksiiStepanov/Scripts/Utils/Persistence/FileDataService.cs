using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OleksiiStepanov.Utils.Persistence
{
    public class FileDataService : IDataService
    {
        private ISerializer serializer;
        private string dataPath;
        private string fileExtension;

        public FileDataService(ISerializer serializer)
        {
            this.serializer = serializer;

            dataPath = Application.persistentDataPath;
            fileExtension = "json";
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(dataPath, string.Concat(fileName, ".", fileExtension));
        }

        public void Save(GameData gameData, bool overwrite = true)
        {
            string fileLocation = GetFilePath(gameData.Name);

            if (!overwrite && File.Exists(fileLocation))
            {
                throw new IOException($"The file '{gameData.Name}.{fileExtension}' already exists and cannot be overwritten.");
            }

            File.WriteAllText(fileLocation, serializer.Serialize(gameData));
        }

        public GameData Load(string saveName)
        {
            string fileLocation = GetFilePath(saveName);

            if (File.Exists(fileLocation))
            {
                throw new IOException($"No persisted GameData with name '{saveName}'");
            }

            return serializer.Deserialize<GameData>(File.ReadAllText(fileExtension));
        }

        public void Delete(string saveName)
        {
            string fileLocation = GetFilePath(saveName);

            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
            }
        }

        public void DeleteAll()
        {
            foreach(string filePath in Directory.GetFiles(dataPath))
            {
                File.Delete(filePath);
            }
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(dataPath))
            {
                if (Path.GetExtension(path) == fileExtension)
                {
                    yield return Path.GetFileNameWithoutExtension(path);
                }
            }
        }
    }
}

