using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;


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
	        _services.RegisterSingle<IInputService>(SetupInputServices());
	        _services.RegisterSingle<IAssets>(new AssetsProvider());
	        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
	        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>() ) );
	        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();

        private IInputService SetupInputServices()
        {
            var _inputServices = new NewInputSystemService();
            _inputServices.Init();
            return _inputServices;
        }
    }
}