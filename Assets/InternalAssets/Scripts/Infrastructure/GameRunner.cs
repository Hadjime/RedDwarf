using System;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper bootstrapperPrefab;
        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
                Instantiate(bootstrapperPrefab);
        }
    }
}