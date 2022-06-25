using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure
{
    public class Game
    {
        public static IInputService InputServices;
        public GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, DiContainer diContainer)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, diContainer);
        }

        
    }
}