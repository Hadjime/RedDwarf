using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Services.Input
{
	public class StandardInputService : InputService
	{
		public override Vector2 RawMovementInput
		{
			get
			{
				Vector2 rawMovementInput = UnityAxis();
				
				return rawMovementInput;
			}
		}


		
	}
}
