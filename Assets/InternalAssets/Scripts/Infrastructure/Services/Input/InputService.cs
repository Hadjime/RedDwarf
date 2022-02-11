﻿using System;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
	public abstract class InputService : IInputService
	{
		protected const string HORIZONTAL = "Horizontal";
		protected const string VERTICAL = "Vertical";

		public abstract Vector2 RawMovementInput { get; }


		public bool IsAttackBtnUp()
		{
			return UnityEngine.Input.GetMouseButtonUp(0);
		}

		public event Action<Vector2> MovementDirectionChanged;

		protected void InvokeEvent(Vector2 movementDirection) =>
			MovementDirectionChanged?.Invoke(movementDirection);

		protected static Vector2 UnityAxis() =>
			new Vector2(UnityEngine.Input.GetAxis(HORIZONTAL), UnityEngine.Input.GetAxis(VERTICAL));
	}
}
