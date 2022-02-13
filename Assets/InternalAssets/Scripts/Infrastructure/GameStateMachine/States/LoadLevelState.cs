using System.Diagnostics;
using Cinemachine;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Player;
using InternalAssets.Scripts.UI;
using InternalAssets.Scripts.UI.GamePlay;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";
        private const string ROOT_UI_TAG = "RootUI";
		private const string Enemyspawner = "EnemySpawner";


		private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
        }

        private void InitGameWorld()
		{
			InitSpawners();
            GameObject hero = InitHero();
            InitHud(hero);
        }


		private void InitSpawners()
		{
			foreach (GameObject spawnerObject in GameObject.FindGameObjectsWithTag(Enemyspawner))
			{
				EnemySpawner enemySpawner = spawnerObject.GetComponent<EnemySpawner>();
				_gameFactory.Register(enemySpawner);
			}
		}


		private GameObject InitHero()
        {
            var initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            var hero = _gameFactory.CreateHero(at: initialPoint);
            SetCameraFollow(hero.transform);

            return hero;
        }

        private void InitHud(GameObject hero)
        {
            var RootObjectForHud = GameObject.FindWithTag(ROOT_UI_TAG);
            var Hud = _gameFactory.CreateHud();
            Hud.transform.SetParent(RootObjectForHud.transform, false);
            
            HeroHealth heroHealth = hero.GetComponent<HeroHealth>();
            Hud.GetComponentInChildren<GamePlayPanel>().Constructor(heroHealth);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void SetCameraFollow(Transform player)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = player;
        }
    }
}