using InternalAssets.Scripts.Characters.Hero;
using Zenject;


namespace InternalAssets.Scripts.Infrastructure.GameStateMachine.States
{
    public class GameLoopState : IPayloadState<HeroDeath>
    {
        private readonly LazyInject<IGameStateMachine> _gameStateMachine;
		private HeroDeath _heroDeath;

		public GameLoopState(LazyInject<IGameStateMachine> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

		public void Enter(HeroDeath heroDeath)
		{
			_heroDeath = heroDeath;
			_heroDeath.HeroDead += OnHeroDead;
		}

		public void Exit()
        {
			_heroDeath.HeroDead -= OnHeroDead;
        }

		private void OnHeroDead() =>
			_gameStateMachine.Value.Enter<BootstrapState>();
	}
}