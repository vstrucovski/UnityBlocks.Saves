using UnityBlocks.SaveSystem.Modules.Starages;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Modules.Example
{
    public class ExampleData : MonoBehaviour
    {
        private SaveService saveService;

        private void Start()
        {
            saveService = new SaveService(new PlayerPrefsDataStorage());
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