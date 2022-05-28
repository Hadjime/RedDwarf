using System;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 NormalizeMovementInput { get; }

		event Action<Vector2> MovementDirectionChanged;
		event  Action IsAttackBtnUp;
	}
}