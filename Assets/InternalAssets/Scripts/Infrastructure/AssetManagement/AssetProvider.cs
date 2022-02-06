using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.InstantiateAsync(path);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            return player;
        }

        public GameObject Instantiate(string path, GameObject at)
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.InstantiateAsync(path, at.transform.position, Quaternion.identity);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            return player;
        }
    }
}