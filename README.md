### 1. Init
```csharp

public class MyComponent : MonoBehaviour
{
    private SaveService saveService;

    private void Start()
    {
        //manual creation should be replaced with DI 
        saveService = new SaveService(new PlayerPrefsDataStorage());
    }
}
```
### 2. Loading Data
Before accessing data, ensure it is prepared using PrepareData<T>():
```csharp
public void RetrieveSomeData()
{
    var myCustomData = saveService.GetData<MyCustomData>();
}
```


### 3. Saving Data
```csharp
public void SaveChanges()
{
    var myCustomData = ...
    myCustomData.NotifyChanges(); // optional
    saveService.Save(myCustomData);
}
```