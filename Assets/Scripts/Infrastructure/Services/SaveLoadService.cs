using Infrastructure.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class SaveLoadService 
    {
        private const string GameDataPrefsKey = "GameData";

        public void SaveProgress(GameData gameData)
        {
            string json = JsonUtility.ToJson(gameData, true);
            PlayerPrefs.SetString(GameDataPrefsKey, json);

            Debug.Log($"Game state saved {json}");
        }

        public GameData LoadProgress()
        {
            string json = PlayerPrefs.GetString(GameDataPrefsKey);
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
