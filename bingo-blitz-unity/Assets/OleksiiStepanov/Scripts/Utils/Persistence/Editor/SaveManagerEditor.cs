using UnityEditor;
using UnityEngine;

namespace OleksiiStepanov.Utils.Persistence.Editor
{
    [CustomEditor(typeof(DataPersistenceManager))]
    public class SaveManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DataPersistenceManager saveLoadManager = (DataPersistenceManager)target;
            string gameName = saveLoadManager.gameData.Name;

            DrawDefaultInspector();

            if (GUILayout.Button("Save Game"))
            {
                saveLoadManager.SaveGame();
            }

            if (GUILayout.Button("Load Game"))
            {
                saveLoadManager.LoadGame(gameName);
            }

            if (GUILayout.Button("Delete Game"))
            {
                saveLoadManager.DeleteSave(gameName);
            }
        }
    }
}
