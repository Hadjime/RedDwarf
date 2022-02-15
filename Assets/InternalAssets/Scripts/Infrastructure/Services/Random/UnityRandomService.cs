using UnityEngine;


namespace InternalAssets.Scripts.Infrastructure.Services.Random
{
	public class UnityRandomService: IRandomService {
		public int Next(int min, int max) =>
			UnityEngine.Random.Range(minInclusive: min, maxExclusive: max);
	}
}
