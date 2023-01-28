using InternalAssets.Scripts.Infrastructure.Scene;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure.GameStateMachine.States
{
    public class BootstrapState: IState
    {
		private const string InitialSceneName = "Initial";
		
		
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
		private readonly SceneLoader _sceneLoader;


		public BootstrapState(LazyInject<IGameStateMachine> gameStateMachine, SceneLoader sceneLoader)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
		}

        public void Enter()
        {
	        _sceneLoader.Load(InitialSceneName, onLoaded: EnterInLoadLevel);
        }


        public void Exit()
        {
            
        }


		private void EnterInLoadLevel() =>
			_gameStateMachine.Value.Enter<LoadProgressState>();


	// 	private void RegisterServices()
	// 	{
	// 		RegisterAdsService();
	//
	// 		_services.RegisterSingle<IGameStateMachine>(_stateMachine);
	// 		
	// 		RegisterAssetProvider();
	// 		
	// 		_services.RegisterSingle<IRandomService>(new UnityRandomService());
	// 		_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
	//
	// 		RegisterIAPService(new IAPProvider(), _services.Single<IPersistentProgressService>());
	// 		RegisterStaticDataService();
	// 		
	// 		RegisterWindowsServiceAndUIFactory();
	//
	// 		_services.RegisterSingle<IInputService>(SetupInputServices());
	// 		_services.RegisterSingle<IGameFactory>(new GameFactory(
	// 			_services.Single<IAssets>(),
	// 			_services.Single<IStaticDataService>(),
	// 			_services.Single<IRandomService>(),
	// 			_services.Single<IPersistentProgressService>(),
	// 			_services.Single<IWindowService>()) );
	// 		
	// 		_services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
	// 			_services.Single<IPersistentProgressService>(),
	// 			_services.Single<IGameFactory>()));
	// 		
	// 		SRDebug.Instance.AddOptionContainer(new CheatsThroughDI(
	// 			_services.Single<IPersistentProgressService>(),
	// 			_services.Single<ISaveLoadService>(),
	// 			_services.Single<IGameFactory>()));
	// 	}
	//
	//
	// 	private void RegisterAssetProvider()
	// 	{
	// 		var assetsProvider = new AssetsProvider();
	// 		assetsProvider.Initialize();
	// 		_services.RegisterSingle<IAssets>(assetsProvider);
	// 	}
	//
	//
	// 	private void RegisterStaticDataService()
	// 	{
	// 		IStaticDataService staticDataService = new StaticDataService(_services.Single<IAssets>());
	// 		staticDataService.Load();
	// 		_services.RegisterSingle<IStaticDataService>(staticDataService);
	// 	}
	//
	//
	// 	private void RegisterWindowsServiceAndUIFactory()
	// 	{
	// 		var windowService = new WindowService();
	// 		_services.RegisterSingle<IWindowService>(windowService);
	//
	// 		UIFactory uiFactory = new UIFactory(
	// 			_services.Single<IAssets>(),
	// 			_services.Single<IStaticDataService>(),
	// 			_services.Single<IPersistentProgressService>(),
	// 			_services.Single<IAdsService>(),
	// 			_services.Single<IWindowService>());
	// 		_services.RegisterSingle<IUIFactory>(uiFactory);
	//
	// 		windowService.Initialize(uiFactory);
	// 	}
	//
	//
	// 	private IInputService SetupInputServices()
	// 	{
	// 		var _inputServices = new NewInputSystemService();
	// 		_inputServices.Init();
	// 		return _inputServices;
	// 	}
	//
	//
	// 	private void RegisterAdsService()
	// 	{
	// 		IAdsService adsService = new AdsService();
	// 		adsService.Initialize(true);
	// 		_services.RegisterSingle<IAdsService>(adsService);
	// 	}
	//
	//
	// 	private void RegisterIAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
	// 	{
	// 		IAPService iapService = new IAPService(iapProvider, progressService);
	// 		iapService.Initialize();
	// 		_services.RegisterSingle<IIAPService>(iapService);
	// 	}
	}
}