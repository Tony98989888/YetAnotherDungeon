using System.IO;
using UnityEngine;

namespace UserCreation
{
    public class UserDataHandler
    {
        // Save structure should be like ../Save/User_1
        public static string CreateNewUser()
        {
            string saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }


            int count = Directory.GetFiles(saveDirectory).Length;
            string newSaveName = string.Concat("User_", count);

            string playerSaveDirectory = Path.Combine(saveDirectory, newSaveName);

            if (!Directory.Exists(playerSaveDirectory))
            {
                Directory.CreateDirectory(playerSaveDirectory);
                Debug.Log($"Save directory for player {newSaveName} created at {playerSaveDirectory}");
                return playerSaveDirectory;
            }
            else
            {
                return null;
            }
        }

        public static void SavePlayerData(PlayerData data)
        {
            string json = JsonUtility.ToJson(data);
            string playerSaveDirectory = data.SavePath;
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
        public string SavePath;
    }
}