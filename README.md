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
        //manual creation should be replaced with DI 
        saveService = new SaveService(new PlayerPrefsDataStorage(), default);
    }
}
```
### 3. Loading Data
Before accessing data, ensure it is prepared using PrepareData<T>():
```csharp
public void RetrieveSomeData()
{
    var myCustomData = saveService.GetData<MyCustomData>();
}
```


### 4. Saving Data
```csharp
public void SaveChanges()
{
    var myCustomData = ...
    myCustomData.NotifyChanges(); // optional
    saveService.Save(myCustomData);
}
```
### 5. Advanced
#### 5.1 Zenject
- add ZENJECT to Scripting Define Symbols (Project Settings > Player > Other Settings)
- Add service installer **ZenjectSaveServiceInstaller.cs** to project or scene scope
- Create config via project's menu "Unity Blocks/Save System" and assign to installer above
