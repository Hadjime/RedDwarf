using System;
using InternalAssets.Scripts.Player.Data;
using InternalAssets.Scripts.Player.PlayerStates;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        private PlayerFSM _playerFsm;

        #region Player States

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        
        #endregion

        public void Awake()
        {
            _playerFsm = new PlayerFSM();
            _idleState = new PlayerIdleState(this, _playerFsm, playerData, "idle");
            _moveState = new PlayerMoveState(this, _playerFsm, playerData, "move");
        }
    }
}