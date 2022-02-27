﻿using InternalAssets.Scripts.Cheats;
using InternalAssets.Scripts.Infrastructure.Ads;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.UI.Services.Factory;
using InternalAssets.Scripts.UI.Services.Windows;


namespace InternalAssets.Scripts.Infrastructure.States
{
    public class BootstrapState: IState
    {
		private const string InitialSceneName = "Initial";
		
		
        private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private AllServices _services;


		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			
			RegisterServices();
		}

        public void Enter()
        {
	        _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }


        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
			RegisterAdsService();

			_services.RegisterSingle<IAssets>(new AssetsProvider());
			_services.RegisterSingle<IRandomService>(new UnityRandomService());
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

			RegisterStaticDataService();

			_services.RegisterSingle<IUIFactory>(new UIFactory(
				_services.Single<IAssets>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<IAdsService>()));
			_services.RegisterSingle<IWindowService>(
				new WindowService(_services.Single<IUIFactory>()));

			_services.RegisterSingle<IInputService>(SetupInputServices());
			_services.RegisterSingle<IGameFactory>(new GameFactory(
				_services.Single<IAssets>(),
				_services.Single<IStaticDataService>(),
				_services.Single<IRandomService>(),
				_services.Single<IPersistentProgressService>(),
				_services.Single<IWindowService>()) );
			
			_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
				_services.Single<IPersistentProgressService>(),
				_services.Single<IGameFactory>()));
			
			SRDebug.Instance.AddOptionContainer(new CheatsThroughDI(
				_services.Single<IPersistentProgressService>(),
				_services.Single<ISaveLoadService>()));
		}


		private void RegisterAdsService()
		{
			IAdsService adsService = new AdsService();
			adsService.Initialize(true);
			_services.RegisterSingle<IAdsService>(adsService);
		}


		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();


		private IInputService SetupInputServices()
        {
            var _inputServices = new NewInputSystemService();
            _inputServices.Init();
            return _inputServices;
        }


		private void RegisterStaticDataService()
		{
			IStaticDataService staticDataService = new StaticDataService(_services.Single<IAssets>());
			staticDataService.Load();
			_services.RegisterSingle<IStaticDataService>(staticDataService);
		}
	}
}