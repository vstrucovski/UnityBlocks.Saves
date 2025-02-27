using System;
using System.IO;
using System.Text;
using UnityBlocks.SaveSystem.Modules.Data;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Modules.Storages
{
    public class BinaryDataStorage : IDataStorage
    {
        private readonly string savePath;

        public BinaryDataStorage()
        {
            savePath = Application.persistentDataPath + "/saves/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        private string GetFilePath<T>() where T : ISavable
        {
            return Path.Combine(savePath, typeof(T).ToString() + ".bin");
        }

        public T Load<T>() where T : ISavable, new()
        {
            var filePath = GetFilePath<T>();
            if (!File.Exists(filePath))
            {
                Debug.Log($"No save file found for {typeof(T)}, returning default.");
                return default;
            }

            try
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                string json = Encoding.UTF8.GetString(fileData);
                Debug.Log($"Loaded binary JSON for {typeof(T)}: {json}");
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load binary JSON for {typeof(T)}: {e.Message}");
                return default;
            }
        }

        public void Save<T>(T saveData) where T : ISavable
        {
            string filePath = GetFilePath<T>();
            try
            {
                var json = JsonUtility.ToJson(saveData);
                byte[] fileData = Encoding.UTF8.GetBytes(json);
                File.WriteAllBytes(filePath, fileData);
                Debug.Log($"Saved binary JSON for {typeof(T)}: {json}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save binary JSON for {typeof(T)}: {e.Message}");
            }
        }

        public void Delete<T>() where T : ISavable
        {
            string filePath = GetFilePath<T>();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"Deleted binary save for {typeof(T)}.");
            }
            else
            {
                Debug.LogWarning($"Attempted to delete non-existent save for {typeof(T)}.");
            }
        }
    }
}