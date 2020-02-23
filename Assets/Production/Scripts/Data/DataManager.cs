using System.IO;
using Production.Plugins.RyanScriptableObjects.SOEvents.GameDataEvent;
using UnityEngine;

namespace Production.Scripts
{
    public class DataManager: MonoBehaviour
    {
        private GameData GameData;
        public GameDataEvent OnSaveEvent;
        public GameDataEvent OnLoadEvent;

        private void Awake()
        {
            GameData gameData = Load();
            if (gameData.NewGame)
            {
                LaunchLoad();
            }
            else
            {
                Debug.Log("No GameData save founded");
            }
        }

        public void LaunchSave()
        {
            OnSaveEvent.Raise(GameData);
            Save(GameData);
        }
        
        public void LaunchLoad()
        {
            OnLoadEvent.Raise(Load());
        }
        public void Save(GameData gameData)
        {
            gameData.NewGame = false;
            string saveJson = JsonUtility.ToJson(gameData);
            File.WriteAllText(Application.streamingAssetsPath + "/save.txt", saveJson);
            Debug.Log("GameSaved");
        }
        public GameData Load()
        {
            GameData loadedData = new GameData();
            if (File.Exists(Application.streamingAssetsPath + "/save.txt"))
            {
                string LoadJson = File.ReadAllText(Application.streamingAssetsPath + "/save.txt");
                loadedData = JsonUtility.FromJson<GameData>(LoadJson);
                
            }
            return loadedData;
        }
        
    }
}