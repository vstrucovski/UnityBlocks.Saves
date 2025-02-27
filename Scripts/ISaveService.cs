using UnityBlocks.SaveSystem.Data;

namespace UnityBlocks.SaveSystem
{
    public interface ISaveService
    {
        T GetData<T>() where T : ISavable, new();
        void SaveData<T>(T value) where T : ISavable;
        bool DeleteData<T>() where T : ISavable;
    }
}