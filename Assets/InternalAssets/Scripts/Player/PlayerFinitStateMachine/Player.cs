using System;
using InternalAssets.Scripts.Player.Data;
using InternalAssets.Scripts.Player.Input;
using InternalAssets.Scripts.Player.PlayerStates;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        public PlayerFSM _playerFSM { get; private set; }
        public PlayerInputHandler _inputHandler { get; private set; }

        #region Player States

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        
        #endregion

        public void Awake()
        {
            _playerFSM = new PlayerFSM();
            _idleState = new PlayerIdleState(this, _playerFSM, playerData, "idle");
            _moveState = new PlayerMoveState(this, _playerFSM, playerData, "move");
        }
    }
}