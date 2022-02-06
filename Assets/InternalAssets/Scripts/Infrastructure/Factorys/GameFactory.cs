using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string PLAYER_WITH_SERVICE_PATH = "PlayerWithService";
        private const string HUD_CANVAS_PATH = "HUDCanvas";

        public GameObject CreateHero(GameObject at)
        {
            var player = Instantiate(PLAYER_WITH_SERVICE_PATH, at: at);
            return player;
        }

        public GameObject CreateHud()
        {
            return Instantiate(HUD_CANVAS_PATH);
        }

        private static GameObject Instantiate(string path)
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.InstantiateAsync(path);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            return player;
        }

        private static GameObject Instantiate(string path, GameObject at)
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.InstantiateAsync(path, at.transform.position, Quaternion.identity);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            return player;
        }
    }
}