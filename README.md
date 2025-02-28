## 🚀 Advantages
 
- **🎯 Flexible** – Works with different storage solutions (e.g., `PlayerPrefs`, json).
- **📦 DI-Friendly** – Ready to Zenject integration for better scalability.
- **👀 Auto-Tracking** – Observable data wrappers let you react to changes instantly.
- **⚡ Fast & Lightweight** – Optimized dictionary-based data retrieval.

## 1. Installation
To add this plugin to a project just put this line into the manifest file
```json
"unityblocks.saves" : "https://github.com/vstrucovski/UnityBlocks.Saves.git"
```

Prepare your data
```csharp
    //Pure data class to save
    [Serializable]
    public class GameProgress
    {
        public float money;
        public float experience;
    }

    //Wrapper for pure class with Observable inside
    public class CustomSavable : SavableData<GameProgress>{}
```
---
## 2. Usages
Method **GetData()** uses dictionary under the hood, so it's almost free to get without additional caching. <br>
Before accessing data, it could be warmed using method **PrepareData<T>()**
```csharp
//manual creating 
saveService = new SaveService(new PlayerPrefsDataStorage(), default);
 
// 💾 Loading Data. Before accessing data, it could be warmed using PrepareData<T>()
saveService.PrepareData<CustomSavable>() //optional: uses once during loading
var myCustomData = saveService.GetData<CustomSavable>(); 
 
// 💾 Saving Data
saveService.Save(myCustomData);
myCustomData.NotifyChanges(); // Optional: Use this if your data model requires change tracking
```
---
## 3. Using as service with DI
- Create a class for the service installer (ZenjectSaveServiceInstaller.cs) and add it to the project or scene scope.
- Create config via project's menu **Create > Unity Blocks > Saves > Config** and assign it to installer
```csharp
using UnityBlocks.SaveSystem;
using UnityBlocks.SaveSystem.Data;
using UnityBlocks.SaveSystem.Storages;
using UnityBlocks.SaveSystem.Storages.Impl;
using UnityEngine;
using Zenject;

namespace MyGame.Installers
{
    public class ZenjectSaveServiceInstaller :MonoInstaller
    {
        [SerializeField] private SaveServiceConfig config;
        
        public override void InstallBindings()
        {
            
            Container.BindInstance(config);
            Container.Bind<IDataStorage>().To<PlayerPrefsDataStorage>().AsSingle();
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
        }
    }
}
```
---
# TODO
- encryption
- more storages
- mark data as dirty without real writing to file every time