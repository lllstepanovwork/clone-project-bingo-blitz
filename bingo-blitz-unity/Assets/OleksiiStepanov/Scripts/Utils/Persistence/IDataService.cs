using System.Collections.Generic;

namespace OleksiiStepanov.Utils.Persistence
{
    public interface IDataService
    {
        void Save(GameData gameData, bool overwrite = true);
        GameData Load(string saveName);
        void Delete(string saveName);
        void DeleteAll();
        IEnumerable<string> ListSaves();
    }
}

