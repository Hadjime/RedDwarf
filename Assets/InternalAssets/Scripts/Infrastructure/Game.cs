using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure
{
	public class Game
	{
		public static IInputService InputServices;
		public GameStateMachine.GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, DiContainer diContainer)
		{
			StateMachine = new GameStateMachine.GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, diContainer);
		}
	}
}
