using System;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
	public abstract class InputService : IInputService
	{
		protected const string HORIZONTAL = "Horizontal";
		protected const string VERTICAL = "Vertical";

		public abstract Vector2 RawMovementInput { get; }


		public event Action<Vector2> MovementDirectionChanged;
		public event Action IsAttackBtnUp;

		protected void InvokeEventMovement(Vector2 movementDirection) =>
			MovementDirectionChanged?.Invoke(movementDirection);
		
		protected void InvokeEventAttack() => 
			IsAttackBtnUp?.Invoke();

		protected static Vector2 UnityAxis() =>
			new Vector2(UnityEngine.Input.GetAxis(HORIZONTAL), UnityEngine.Input.GetAxis(VERTICAL));
	}
}
