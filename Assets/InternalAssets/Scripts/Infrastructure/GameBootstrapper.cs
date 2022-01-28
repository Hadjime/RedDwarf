using System;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
			CustomDebug.Log($"[Game] Init", Color.grey);
        }
    }
}