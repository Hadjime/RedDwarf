using UnityEngine;
using UnityEngine.InputSystem;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
	public class NewInputSystemService : InputService
	{
		private PlayerInputSystem _playerInput;
		private Vector2 _rawMovementInput;
		

		public void Init()
		{
			_playerInput = new PlayerInputSystem();

			_playerInput.Enable();
			_playerInput.Player.Movement.started += OnMovementStarted;
		}

		
		private void OnMovementStarted(InputAction.CallbackContext context)
		{
			if(context.started)
			{
				_rawMovementInput = context.ReadValue<Vector2>();
			}
		}

		public override Vector2 RawMovementInput => _rawMovementInput;
	}
}
