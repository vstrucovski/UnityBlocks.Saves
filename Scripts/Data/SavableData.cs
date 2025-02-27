using System;
using UnityEngine;

namespace UnityBlocks.SaveSystem.Modules.Data
{
    [Serializable]
    public class SavableData<T> : ISavable where T : new()
    {
        [SerializeField] private T _value;

        public event Action<T> OnChanged;

        public SavableData()
        {
            _value = new T();
        }

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    NotifyChanges();
                }
            }
        }

        public void NotifyChanges()
        {
            OnChanged?.Invoke(_value);
        }
    }
}