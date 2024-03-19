using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UserCreation
{
    public class UserDataHandler
    {
        public static List<int> ToInts(List<string> occupiedSlots)
        {
            return occupiedSlots
                .Select(s => Int32.TryParse(s, out int n) ? n : (int?)null)
                .Where(n => n.HasValue)
                .Select(n => n.Value)
                .ToList();
        }

        public static int NextAvailableSaveSlot(List<int> occupiedSlots)
        {
            // 将列表中的所有元素添加到HashSet中，以便O(1)复杂度的访问
            HashSet<int> set = new HashSet<int>(occupiedSlots);

            // 从1开始遍历，1是第一个正整数
            int slotIndex = 1;

            // 如果当前数字在集合中，那么缺失的数字一定比当前数字大，继续检查下一个数字
            while (set.Contains(slotIndex))
            {
                slotIndex++;
            }

            // 一旦找到不在集合中的第一个正整数，即为我们要找的缺失数字
            return slotIndex;
        }

        // Save structure should be like ../Save/User_1
        public static string CreateNewUser()
        {
            string saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            var subDirs = Directory.GetDirectories(saveDirectory)
                .Select(Path.GetFileName)
                .ToList();

            subDirs.ForEach(x => x.Replace("User_", ""));

            List<string> res = new List<string>();
            foreach (var dir in subDirs)
            {
                res.Add(dir.Replace("User_", ""));
            }

            var occupiedSlots = UserDataHandler.ToInts(res);

            var slotIndex = UserDataHandler.NextAvailableSaveSlot(occupiedSlots);

            string newSaveName = string.Concat("User_", slotIndex);

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

        public static List<PlayerData> LoadAllSaves()
        {
            string saveDirectory = Path.Combine(Application.persistentDataPath, "Saves");
            int count = Directory.GetFiles(saveDirectory).Length;


            var subdirs = Directory.GetDirectories(saveDirectory)
                .Select(Path.GetFileName)
                .ToList();

            List<PlayerData> res = new List<PlayerData>();

            foreach (var dir in subdirs)
            {
                string userSaveDir = Path.Combine(saveDirectory, dir, "playerData.json");
                PlayerData data = JsonUtility.FromJson<PlayerData>(File.ReadAllText(userSaveDir));
                res.Add(data);
            }

            return res;
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