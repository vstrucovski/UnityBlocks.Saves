using System;
using System.Collections.Concurrent;
using UnityBlocks.SaveSystem.Data;
using UnityBlocks.SaveSystem.Storages;

namespace UnityBlocks.SaveSystem
{
    public class SaveService : ISaveService
    {
        private readonly IDataStorage _dataStorage;
        private readonly ConcurrentDictionary<Type, object> _saveDataMap = new();
        private SaveServiceConfig _config;

        public SaveService(IDataStorage dataStorage, SaveServiceConfig config)
        {
            if (config == null) config = default;
            _dataStorage = dataStorage;
            _config = config;
        }

        public void PrepareData<T>(T defaultValue = default) where T : ISavable, new()
        {
            var loadedData = _dataStorage.Load<T>();
            if (loadedData != null)
            {
                _saveDataMap[typeof(T)] = loadedData;
            }
            else
            {
                defaultValue ??= new T();
                _saveDataMap[typeof(T)] = defaultValue;
            }
        }

        public T GetData<T>() where T : ISavable, new()
        {
            if (_saveDataMap.TryGetValue(typeof(T), out var saveData))
            {
                return (T) saveData;
            }

            PrepareData<T>();
            return (T) _saveDataMap[typeof(T)];
        }

        public void SaveData<T>(T value) where T : ISavable
        {
            _saveDataMap[typeof(T)] = value;
            _dataStorage.Save(value);
        }

        public bool DeleteData<T>() where T : ISavable
        {
            _dataStorage.Delete<T>();
            return _saveDataMap.TryRemove(typeof(T), out _);
        }
    }
}