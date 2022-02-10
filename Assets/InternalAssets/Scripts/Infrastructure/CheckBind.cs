using System;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using Zenject;

namespace InternalAssets.Scripts.Infrastructure
{
    public class CheckBind : MonoBehaviour
    {
        private IAssets _assets;

        [Inject]
        public void Constructor(IAssets assets)
        {
            _assets = assets;
            CustomDebug.Log($"Check bind complete", Color.magenta);
        }

        private void Awake()
        {
            CustomDebug.Log($"Awake", Color.magenta);
        }
    }
}