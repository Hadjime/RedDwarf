using System.Threading.Tasks;
using Cinemachine;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.Map.Grids;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.UI.Services.Factory;
using InternalAssets.Scripts.UI.Windows.GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


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
		private readonly IInputService _inputService;
		private GameObject hero;


		public LoadSceneState(
				GameStateMachine stateMachine,
				SceneLoader sceneLoader,
				IGameFactory gameFactory,
				IPersistentProgressService progressService,
				IStaticDataService staticDataService,
				IUIFactory uiFactory,
				IInputService inputService
			)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
			_staticDataService = staticDataService;
			_uiFactory = uiFactory;
			_inputService = inputService;
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
			await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();
			
			if (hero.TryGetComponent(out HeroDeath heroDeath))
				_stateMachine.Enter<GameLoopState, HeroDeath>(heroDeath);
        }


		private async Task InitUIRoot() =>
			await _uiFactory.CreateUIRoot();


		private async Task InitGameWorld()
		{
			LevelStaticData levelData = LevelStaticData();

			GridsManager gridsManager = await InitGrid(levelData);
			await InitSpawners(levelData);
			await InitDroppedLoot(); // TODO сделать если лут выпал но не был собран и перезайти в игру то добавляем на сцену
            hero = await InitHero(levelData, gridsManager);
            await InitHud(hero);
        }

		private async Task<GridsManager> InitGrid(LevelStaticData levelStaticData)
		{
			GridsManager gridsManager = await _gameFactory.CreateGrid(levelStaticData.Grid);
			gridsManager.Initialize(_progressService);
			return gridsManager;
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


		private async Task<GameObject> InitHero(LevelStaticData levelStaticData, GridsManager gridsManager)
        {
			GameObject hero = await _gameFactory.CreateHero(at: levelStaticData.InitialHeroPosition);
			if (hero.TryGetComponent(out HeroMove heroMove))
				heroMove.Construct(_inputService);

			if (hero.TryGetComponent(out HeroAttack heroAttack))
				heroAttack.Construct(_inputService);
			
			if (hero.TryGetComponent(out DestroyFogOfWar destroyFogOfWar))
			{
				destroyFogOfWar.grid = gridsManager.Grid;
				destroyFogOfWar.fogOfWar = gridsManager.FogOfWar;
			}
			
            SetCameraFollow(hero.transform);
			SetCameraConfiner(gridsManager.Land);
			
            return hero;
        }


		private async Task InitHud(GameObject hero)
        {
			GameObject hud = await _uiFactory.CreateHud();

			HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
			HeroAttack heroAttack = hero.GetComponent<HeroAttack>();
			hud.GetComponentInChildren<GamePlayPanel>().Constructor(heroHealth, heroAttack);
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

		private void SetCameraConfiner(Tilemap Land)
		{
			CinemachineConfiner cinemachineConfiner = Camera.main.GetComponentInChildren<CinemachineConfiner>();
			if (Land.TryGetComponent(out CompositeCollider2D collider2D))
				cinemachineConfiner.m_BoundingShape2D = collider2D;
		}
    }
}