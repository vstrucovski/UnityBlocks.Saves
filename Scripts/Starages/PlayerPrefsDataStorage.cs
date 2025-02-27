using UnityBlocks.SaveSystem.Modules.Data;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Modules.Starages
{
    public class PlayerPrefsDataStorage : IDataStorage
    {
        public T Load<T>() where T : ISavable, new()
        {
            var key = typeof(T).ToString();
            var json = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                Debug.Log($"Loaded json for {key}: Default");
                return default;
            }

            Debug.Log($"Loaded json for {key}: {json}");
            var parsed = JsonUtility.FromJson<T>(json);
            return parsed;
        }

        public void Save<T>(T data) where T : ISavable
        {
            var json = JsonUtility.ToJson(data);
            var key = typeof(T).ToString();
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
            Debug.Log($"Saved data for {key}: {json}");
        }
        
        public void Delete<T>() where T : ISavable
        {
            var key = typeof(T).ToString();
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            Debug.Log($"Delete data for {key}");
        }
    }
}