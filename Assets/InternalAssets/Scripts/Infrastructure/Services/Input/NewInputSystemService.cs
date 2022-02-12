using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
	public class NewInputSystemService : InputService
	{
		private PlayerInputSystem _playerInput;
		private Vector2 _movementDirection;
		

		public void Init()
		{
			_playerInput = new PlayerInputSystem();

			_playerInput.Enable();
			_playerInput.Player.Movement.started += OnMovementStarted;
			_playerInput.Player.Fire.started += OnFire;
		}


		private void OnFire(InputAction.CallbackContext context)
		{
			if (!context.started)
				return;
			
			InvokeEventAttack();
		}


		private void OnMovementStarted(InputAction.CallbackContext context)
		{
			if (!context.started) 
				return;
			
			_movementDirection = context.ReadValue<Vector2>();
			InvokeEventMovement(movementDirection: _movementDirection);
		}

		public override Vector2 RawMovementInput => _movementDirection;
	}
}
