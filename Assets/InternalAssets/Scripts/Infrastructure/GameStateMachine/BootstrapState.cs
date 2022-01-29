using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Services.Input;


namespace InternalAssets.Scripts.Infrastructure
{
    public class BootstrapState: IState
    {
		private const string Initial = "Initial";
		
		
        private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;


		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
		}

        public void Enter()
        {
            RegisterServices();
			
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }


		private void EnterLoadLevel()
		{
			
		}


		private void RegisterServices()
        {
            Game.InputServices = SetupInputServices();
        }

        public void Exit()
        {
            
        }
        
        private IInputService SetupInputServices()
        {
            var _inputServices = new NewInputSystemService();
            _inputServices.Init();
            return _inputServices;
        }
    }
}