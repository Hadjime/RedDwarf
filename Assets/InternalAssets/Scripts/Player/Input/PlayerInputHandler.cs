using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Player.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _playerInput;

        public Vector2 RawMovementInput { get; private set; };

        public void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        public void Move(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();
        }
    }
}