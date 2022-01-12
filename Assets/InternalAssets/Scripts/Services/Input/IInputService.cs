using UnityEngine;

namespace InternalAssets.Scripts.Services.Input
{
    public interface IInputService
    {
        Vector2 RawMovementInput { get; }
    }
}