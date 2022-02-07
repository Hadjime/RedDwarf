using InternalAssets.Scripts.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at) =>
            _assets.Instantiate(AssetPath.PLAYER_WITH_SERVICE_PATH, at: at);

        public GameObject CreateHud() =>
                _assets.Instantiate(AssetPath.HUD_CANVAS_PATH);
    }
}