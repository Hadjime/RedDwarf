using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;

namespace InternalAssets.Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadSceneState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = 
                _saveLoadService.LoadProgress() 
                ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("Main");
            progress.PlayerState.MaxHp = 100;
            progress.PlayerState.ResetHp();
			progress.HeroStats.Damage = 10;
			progress.HeroStats.DamageRadius = 1.5f;

            return progress;
        }
    }
}