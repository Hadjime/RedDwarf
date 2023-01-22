using InternalAssets.Scripts.Cheats;
using InternalAssets.Scripts.Infrastructure.Ads;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.IAP;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.UI.Services.Factory;
using InternalAssets.Scripts.UI.Services.Windows;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure.Installlers
{
	[CreateAssetMenu(fileName = "ServicesInstaller", menuName = "Installers/ServicesInstaller")]
	public class ServicesInstaller : ScriptableObjectInstaller<ServicesInstaller>
	{
		public override void InstallBindings()
		{
			CustomDebug.Log($"All services installer START bind", Color.green);
			
			RegisterServices();

			CustomDebug.Log($"All services installer STOP bind", Color.green);
		}
		
		private void RegisterServices()
		{
			Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
			
			RegisterAdsService();
			
			//TODO возможно понадобится в дальнейшем и надо подумать как его сюда запихнуть
			// _services.RegisterSingle<IGameStateMachine>(_stateMachine);
			
			RegisterAssetProvider();

			Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
			PersistentProgressService progressService = new PersistentProgressService();
			Container.Bind<IPersistentProgressService>().FromInstance(progressService).AsSingle();

			RegisterIAPService(new IAPProvider(), progressService);
			RegisterStaticDataService();
			
			RegisterWindowsServiceAndUIFactory();
			
			Container.Bind<IInputService>().FromMethod(SetupInputServices).AsSingle();
			GameFactory gameFactory = new GameFactory(
				Container.Resolve<IAssets>(),
				Container.Resolve<IStaticDataService>(),
				Container.Resolve<IRandomService>(),
				Container.Resolve<IPersistentProgressService>(),
				Container.Resolve<IWindowService>());
			Container.Bind<IGameFactory>().FromInstance(gameFactory);

			SaveLoadService saveLoadService = new SaveLoadService(
				Container.Resolve<IPersistentProgressService>(),
				Container.Resolve<IGameFactory>());
			Container.Bind<ISaveLoadService>().FromInstance(saveLoadService);
			
			SRDebug.Instance.AddOptionContainer(new CheatsThroughDI(
				Container.Resolve<IPersistentProgressService>(),
				Container.Resolve<ISaveLoadService>(),
				Container.Resolve<IGameFactory>()));
		}


		private void RegisterAdsService()
		{
			AdsService adsService = new AdsService();
			adsService.Initialize(true);
			Container.Bind<IAdsService>().FromInstance(adsService);
		}

		private void RegisterAssetProvider()
		{
			var assetsProvider = new AssetsProvider();
			assetsProvider.Initialize();
			Container.Bind<IAssets>().FromInstance(assetsProvider).AsSingle();
		}


		private void RegisterIAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
		{
			IAPService iapService = new IAPService(iapProvider, progressService);
			iapService.Initialize();
			Container.Bind<IIAPService>().FromInstance(iapService).AsSingle();
		}

		private void RegisterStaticDataService()
		{
			IStaticDataService staticDataService = new StaticDataService(Container.Resolve<IAssets>());
			staticDataService.Load();
			Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
		}
		
		private void RegisterWindowsServiceAndUIFactory()
		{
			var windowService = new WindowService();
			Container.Bind<IWindowService>().FromInstance(windowService).AsSingle();
			
			UIFactory uiFactory = new UIFactory(
				Container.Resolve<IAssets>(),
				Container.Resolve<IStaticDataService>(),
				Container.Resolve<IPersistentProgressService>(),
				Container.Resolve<IAdsService>(),
				Container.Resolve<IWindowService>());
			Container.Bind<IUIFactory>().FromInstance(uiFactory).AsSingle();

			windowService.Initialize(uiFactory);
		}
		
		private IInputService SetupInputServices()
		{
			var _inputServices = new NewInputSystemService();
			_inputServices.Init();
			return _inputServices;
		}
	}
}