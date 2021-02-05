using System;
using InternalAssets.Scripts.Player.Data;
using InternalAssets.Scripts.Player.Input;
using InternalAssets.Scripts.Player.PlayerStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        public PlayerFSM _playerFSM { get; private set; }
        public PlayerInputHandler _inputHandler { get; private set; }

        public Rigidbody2D _RB2D;
        
        #region Player States

        public PlayerTestState _testState { get; private set; }
        public PlayerIdleState _idleState { get; private set; }
        public PlayerMoveState _moveState { get; private set; }
        
        #endregion

        public void Awake()
        {
            _playerFSM = new PlayerFSM();
            _testState = new PlayerTestState(this, _playerFSM, playerData, "idle");
            _idleState = new PlayerIdleState(this, _playerFSM, playerData, "idle");
            _moveState = new PlayerMoveState(this, _playerFSM, playerData, "move");
        }

        public void Start()
        {
            _RB2D = GetComponent<Rigidbody2D>();
            _inputHandler = GetComponent<PlayerInputHandler>();
            //_playerFSM.Initialize(_testState);
            _playerFSM.Initialize(_idleState);
        }

        public void Update()
        {
            _playerFSM.CurrentState.LogicUpdate();
        }

        public void FixedUpdate()
        {
            _playerFSM.CurrentState.PhysicsUpdate();
        }
    }
}