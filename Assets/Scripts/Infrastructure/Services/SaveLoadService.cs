using Infrastructure.Data;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService 
    {
        public void SaveProgress(GameData gameData, string key)
        {
            string json = JsonUtility.ToJson(gameData, true);
            PlayerPrefs.SetString(key, json);

            Debug.Log($"Game state saved {json}");
        }

        public GameData LoadProgress(string key)
        {
            string json = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(json))
            {
                Debug.Log($"Game state loaded {json}");
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                return gameData;
            }
            return new GameData();
        }
    }
}
