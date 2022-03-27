using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using Zenject;

namespace InternalAssets.Scripts.Infrastructure
{
    public class AllServicesMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
			CustomDebug.Log($"MonoInstaller START bind", Color.green);
			
            Container.Bind<Greeter.Protocol>().FromInstance(new Greeter.Protocol("Mono Installer!"));
            Container.Bind<Greeter>().AsSingle().NonLazy();

            Container.Bind<IAssets>().To<AssetsProvider>().AsSingle();
			
			AllServicesInstaller.Install(Container);

			CustomDebug.Log($"MonoInstaller STOP bind", Color.green);
        }
        
        public class Greeter
        {
            public Greeter(Protocol protoco)
            {
                Debug.Log(protoco.value);
            }


			public class Protocol
			{
				public string value;


				public Protocol(string _value)
				{
					value = _value;
				}
			}
        }

		public class Greeter2
		{
			public Greeter2(Protocol2 protoco)
			{
				Debug.Log(protoco.value);
			}


			public class Protocol2
			{
				public string value;


				public Protocol2(string _value)
				{
					value = _value;
				}
			}
		}
    }

	public class AllServicesInstaller : Installer<AllServicesInstaller>
	{
		public override void InstallBindings()
		{
			CustomDebug.Log($"Installer START bind", Color.green);
			
			Container.Bind<AllServicesMonoInstaller.Greeter2.Protocol2>().FromInstance(new AllServicesMonoInstaller.Greeter2.Protocol2(
				"Simple Installer!"));
			Container.Bind<AllServicesMonoInstaller.Greeter2>().AsSingle().NonLazy();

			Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();

			CustomDebug.Log($"Installer STOP bind", Color.green);
		}
	}
}