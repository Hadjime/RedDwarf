using System;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 RawMovementInput { get; }
		bool IsAttackBtnUp();

		event Action<Vector2> MovementDirectionChanged;
    }
}