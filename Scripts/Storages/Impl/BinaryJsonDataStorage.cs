using System;
using System.IO;
using System.Text;
using UnityBlocks.SaveSystem.Data;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Storages.Impl
{
    public class BinaryJsonDataStorage : IDataStorage
    {
        private readonly string savePath;
        private SaveServiceConfig _config;

        public BinaryJsonDataStorage(SaveServiceConfig config)
        {
            _config = config;
            savePath = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        private string GetFilePath<T>() where T : ISavable
        {
            return Path.Combine(savePath, typeof(T).Name + ".json");
        }

        public T Load<T>() where T : ISavable, new()
        {
            var filePath = GetFilePath<T>();
            if (!File.Exists(filePath))
            {
                if (_config.logRead) Debug.Log($"No save file found for {typeof(T)}, returning default.");
                return default;
            }

            try
            {
                var fileData = File.ReadAllBytes(filePath);
                var json = Encoding.UTF8.GetString(fileData);
                if (_config.logRead) Debug.Log($"Loaded binary JSON for {typeof(T)}: {json}");
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
            var filePath = GetFilePath<T>();
            try
            {
                var json = JsonUtility.ToJson(saveData, true);
                var fileData = Encoding.UTF8.GetBytes(json);
                File.WriteAllBytes(filePath, fileData);
                if (_config.logWrite) Debug.Log($"Saved binary JSON for {typeof(T)}: {json}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save binary JSON for {typeof(T)}: {e.Message} \nto path = {filePath}");
            }
        }

        public void Delete<T>() where T : ISavable
        {
            string filePath = GetFilePath<T>();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                if (_config.logDelete) Debug.Log($"Deleted binary save for {typeof(T)}.");
            }
            else
            {
                if (_config.logDelete) Debug.LogWarning($"Attempted to delete non-existent save for {typeof(T)}.");
            }
        }
    }
}