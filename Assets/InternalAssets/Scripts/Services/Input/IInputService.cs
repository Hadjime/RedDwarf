using UnityEngine;

namespace InternalAssets.Scripts.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 RawMovementInput { get; }
		bool IsAttackBtnUp();
	}
}