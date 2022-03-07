using System.Threading.Tasks;
using Cinemachine;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.UI.Services.Factory;
using InternalAssets.Scripts.UI.Windows.GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Infrastructure.States
{
    public class LoadSceneState : IPayloadState<string>
    {
		private const string Enemyspawner = "EnemySpawner";


		private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
		private readonly IStaticDataService _staticDataService;
		private readonly IUIFactory _uiFactory;


		public LoadSceneState(
				GameStateMachine stateMachine,
				SceneLoader sceneLoader,
				IGameFactory gameFactory,
				IPersistentProgressService progressService,
				IStaticDataService staticDataService,
				IUIFactory uiFactory
			)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
			_staticDataService = staticDataService;
			_uiFactory = uiFactory;
		}

        public void Enter(string sceneName)
        {
            _gameFactory.Cleanup();
			_gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private async void OnLoaded()
		{
			InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
        }


		private void InitUIRoot() =>
			_uiFactory.CreateUIRoot();


		private async Task InitGameWorld()
		{
			LevelStaticData levelData = LevelStaticData();

			await InitSpawners(levelData);
			await InitDroppedLoot(); // TODO если лут выпал но не был собран то добавляем на сцену
            GameObject hero = InitHero(levelData);
            InitHud(hero);
        }


		private async Task InitSpawners(LevelStaticData levelStaticData)
		{
			foreach (EnemySpawnerData spawnerData in levelStaticData.EnemySpawners)
				await _gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position,
					spawnerData.MonsterTypeId);
		}


		private async  Task InitDroppedLoot()
		{
			
		}


		private GameObject InitHero(LevelStaticData levelStaticData)
        {
			var hero = _gameFactory.CreateHero(at: levelStaticData.InitialHeroPosition);
            SetCameraFollow(hero.transform);

            return hero;
        }


		private void InitHud(GameObject hero)
        {
			GameObject hud = _uiFactory.CreateHud();

			HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
			hud.GetComponentInChildren<GamePlayPanel>().Constructor(heroHealth);
        }


		private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }


		private LevelStaticData LevelStaticData() =>
			_staticDataService.ForLevel(SceneManager.GetActiveScene().name);


		private void SetCameraFollow(Transform player)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = player;
        }
    }
}