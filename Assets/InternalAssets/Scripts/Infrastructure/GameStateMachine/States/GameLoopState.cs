using InternalAssets.Scripts.Infrastructure.Scene;

namespace InternalAssets.Scripts.Infrastructure.States
{
    public class GameLoopState : IExitableState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }
    }
}