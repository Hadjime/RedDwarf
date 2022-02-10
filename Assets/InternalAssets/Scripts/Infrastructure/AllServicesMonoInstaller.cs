using InternalAssets.Scripts.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace InternalAssets.Scripts.Infrastructure
{
    public class AllServicesMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<string>().FromInstance("Hello World!");
            Container.Bind<Greeter>().AsSingle().NonLazy();

            Container.Bind<IAssets>().To<AssetsProvider>().AsSingle();
        }
        
        public class Greeter
        {
            public Greeter(string message)
            {
                Debug.Log(message);
            }
        }
    }
}