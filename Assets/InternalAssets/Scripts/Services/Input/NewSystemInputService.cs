using UnityEngine;


namespace InternalAssets.Scripts.Services.Input
{
	public class NewSystemInputService : InputService
	{
		public override Vector2 RawMovementInput => UnityAxis();
	}
}
