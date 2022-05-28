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
			
			_movementDirection = NormalizeInputValue(context.ReadValue<Vector2>());
			
			InvokeEventMovement(movementDirection: _movementDirection);
		}
		
		public override Vector2 NormalizeMovementInput => NormalizeInputValue(_playerInput.Player.Movement.ReadValue<Vector2>());
		// public override Vector2 NormalizeMovementInput => _movementDirection;
		
		private Vector2 NormalizeInputValue(Vector2 movementDirection)
		{
			//отбрасываем меньшее значение, т.к. движение может быть только в одном направлении
			if (Mathf.Abs(movementDirection.x) > Mathf.Abs(movementDirection.y))
				movementDirection.y = 0;
			else
				movementDirection.x = 0;

			return movementDirection;
		}
	}
}
