using UnityBlocks.SaveSystem.Data;

namespace UnityBlocks.SaveSystem.Storages
{
    public interface IDataStorage
    {
        T Load<T>() where T : ISavable, new();
        void Save<T>(T saveData) where T : ISavable;
        void Delete<T>() where T : ISavable;
    }
}