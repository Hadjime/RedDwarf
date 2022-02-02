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
        const string PLAYER_WITH_SERVICE = "PlayerWithService";
        
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

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

            
            AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.InstantiateAsync(PLAYER_WITH_SERVICE, initialPoint.transform.position, Quaternion.identity);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            SetCameraFollow(player.transform);
        }

        private void SetCameraFollow(Transform player)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
            cinemachineVirtualCamera.Follow = player;
        }
    }
}