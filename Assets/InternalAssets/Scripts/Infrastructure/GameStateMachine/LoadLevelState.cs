using System.Diagnostics;
using Cinemachine;
using InternalAssets.Scripts.Infrastructure.Scene;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string INITIAL_POINT_TAG = "InitialPoint";


        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private GameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(INITIAL_POINT_TAG);
            var player = _gameFactory.CreateHero(at: initialPoint);
            SetCameraFollow(player.transform);

            var Hud = _gameFactory.CreateHud();
        }

        private void SetCameraFollow(Transform player)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = player;
        }
    }
}