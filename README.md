### 1. Installation
Add to manifest file line
```json
"unityblocks.saves" : "https://github.com/vstrucovski/UnityBlocks.Saves.git"
```

### 2. Init
```csharp

public class MyComponent : MonoBehaviour
{
    private SaveService saveService;

    private void Start()
    {
        //manual creation should be replaced with DI for better scalability
        saveService = new SaveService(new PlayerPrefsDataStorage(), default);
    }
}
```
### 3. Loading Data 💿
Before accessing data, make sure it is could be warmed using PrepareData<T>()
```csharp
public void RetrieveSomeData()
{
    saveService.PrepareData<MyCustomData>() //optional: uses during loading
    var myCustomData = saveService.GetData<MyCustomData>();
}
```


### 4. Saving Data 💾
```csharp
public void SaveChanges()
{
    var myCustomData = ...
    myCustomData.NotifyChanges(); // Optional: Use this if your data model requires change tracking
    saveService.Save(myCustomData);
}
```
### 5. Using as service with DI 🚀
- Create a class for the service installer (ZenjectSaveServiceInstaller.cs) and add it to the project or scene scope.
- Create config via project's menu "Create/Unity Blocks/Saves/Config" and assign it to installer
```csharp
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
            if(config != null)
                Container.BindInstance(config);
            Container.Bind<IDataStorage>().To<PlayerPrefsDataStorage>().AsSingle();
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
        }
    }
}
```