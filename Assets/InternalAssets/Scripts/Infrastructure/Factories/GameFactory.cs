using InternalAssets.Scripts.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public GameObject CreateHero(GameObject at) =>
            _assetProvider.Instantiate(AssetPath.PLAYER_WITH_SERVICE_PATH, at: at);

        public GameObject CreateHud() =>
                _assetProvider.Instantiate(AssetPath.HUD_CANVAS_PATH);
    }
}