using InternalAssets.Scripts.Services.Input;


namespace InternalAssets.Scripts.Infrastructure
{
    public class BootstrapState: IState
    {
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            RegisterServices();
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