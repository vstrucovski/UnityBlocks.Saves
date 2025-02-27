#if ZENJECT
using Zenject;
namespace UnityBlocks.SaveSystem.Modules.Installers
{
    public class ZenjectSaveServiceInstaller :MonoInstaller<PauseServiceInstaller>
    {
        [SerializeField] private SaveServiceConfig config;
        public override void InstallBindings()
        {
            if(config != null)
                Container.BindInstance(config);
            Container.Bind<IDataStorage>().To<DataStorage>().AsSingle();
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
        }
    }
}
#endif