using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Services;
using InternalAssets.Scripts.Services.Input;
using Unity.Entities;


namespace InternalAssets.Scripts.Infrastructure.States
{
    public class BootstrapState: IState
    {
		private const string Initial = "Initial";
		
		
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
	        _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }


        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
	        _services.RegisterSingle<IInputService>(SetupInputServices());
	        _services.RegisterSingle<IAssets>(new AssetsProvider());
	        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>() ) );
        }

        private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadLevelState, string>("Main");

        private IInputService SetupInputServices()
        {
            var _inputServices = new NewInputSystemService();
            _inputServices.Init();
            return _inputServices;
        }
    }
}