using UnityBlocks.SaveSystem.Data;
using UnityBlocks.SaveSystem.Storages.Impl;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Example
{
    public class ExampleData : MonoBehaviour
    {
        [SerializeField] private SaveServiceConfig config;
        private SaveService saveService;

        private void Start()
        {
            // saveService = new SaveService(new BinaryDataStorage(config), default);
            saveService = new SaveService(new PlayerPrefsDataStorage(config), default);
        }

        public void Load()
        {
            saveService.PrepareData<TestData>();
        }

        public void Save()
        {
            var data = saveService.GetData<TestData>();
            data.NotifyChanges();
            saveService.SaveData(data);
        }

        public void Rand()
        {
            var data = saveService.GetData<TestData>();
            data.Value = Random.Range(0, 100);
        }
    }
}