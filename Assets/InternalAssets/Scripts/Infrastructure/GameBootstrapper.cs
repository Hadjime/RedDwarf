using InternalAssets.Scripts.Infrastructure.GameStateMachine.States;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		private Game _game;
		private DiContainer diContainer;

		[Inject]
		public void Construct(DiContainer diContainer)
		{
			this.diContainer = diContainer;
		}

		private void Awake()
		{
			_game = new Game(this, diContainer);
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
			CustomDebug.Log($"[Game] Init", Color.grey);
		}
	}
}
