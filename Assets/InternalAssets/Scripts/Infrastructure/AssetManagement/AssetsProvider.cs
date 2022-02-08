﻿using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace InternalAssets.Scripts.Infrastructure.AssetManagement
{
    public class AssetsProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.InstantiateAsync(path);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            return player;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle =
                Addressables.InstantiateAsync(path, at, Quaternion.identity);
            GameObject player = asyncOperationHandle.WaitForCompletion();
            return player;
        }
    }
}