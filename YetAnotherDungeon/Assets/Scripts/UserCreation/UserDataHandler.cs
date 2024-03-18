using System.IO;
using UnityEngine;

namespace UserCreation
{
    public class UserDataHandler
    {
        static bool CreateSaveFolder(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Debug.LogError("Player name cannot be empty.");
                return false;
            }
            
            string saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            
            string playerSaveDirectory = Path.Combine(saveDirectory, playerName);
            
            if (!Directory.Exists(playerSaveDirectory))
            {
                Directory.CreateDirectory(playerSaveDirectory);
                Debug.Log($"Save directory for player {playerName} created at {playerSaveDirectory}");
                return true;
            }
            else
            {
                Debug.LogError("A save directory for this player already exists.");
                return false;
            }
        }

        public static void SavePlayerData(PlayerData data)
        {
            string json = JsonUtility.ToJson(data);
            string playerSaveDirectory = Path.Combine(Application.persistentDataPath, "Saves", data.Name);
            string saveFilePath = Path.Combine(playerSaveDirectory, "playerData.json");

            File.WriteAllText(saveFilePath, json);
            Debug.Log($"Player data saved to {saveFilePath}");
        }


        public static PlayerData LoadPlayerData(string playerName)
        {
            string playerSaveDirectory = Path.Combine(Application.persistentDataPath, "Saves", playerName);
            string saveFilePath = Path.Combine(playerSaveDirectory, "playerData.json");

            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                return data;
            }
            else
            {
                Debug.LogError("Save file not found.");
                return null;
            }
        }
    }


    [System.Serializable]
    public class PlayerData
    {
        public string Name;
    }
}