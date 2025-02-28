using UnityBlocks.SaveSystem.Data;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Storages.Impl
{
    public class PlayerPrefsDataStorage : IDataStorage
    {
        private readonly SaveServiceConfig _config;

        public PlayerPrefsDataStorage(SaveServiceConfig config)
        {
            _config = config;
        }

        public T Load<T>() where T : ISavable, new()
        {
            var key = typeof(T).ToString();
            var json = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                if (_config.logRead) Debug.Log($"Loaded json for {key}: Default");
                return default;
            }
            else
            {
                if (_config.logRead) Debug.Log($"Loaded json for {key}: {json}");
            }


            var parsed = JsonUtility.FromJson<T>(json);
            return parsed;
        }

        public void Save<T>(T data) where T : ISavable
        {
            var json = JsonUtility.ToJson(data);
            var key = typeof(T).ToString();
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
            if (_config.logWrite) Debug.Log($"Saved data for {key}: {json}");
        }

        public void Delete<T>() where T : ISavable
        {
            var key = typeof(T).ToString();
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            if (_config.logDelete) Debug.Log($"Delete data for {key}");
        }
    }
}