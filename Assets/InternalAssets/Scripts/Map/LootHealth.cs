using System.Collections;
using InternalAssets.Scripts.Utils;


namespace InternalAssets.Scripts.Map
{
	public class LootHealth : TileHealth
	{
		private float _delay = 0.5f;
		private bool isImmortal = true;


		private void Start()
		{
			isImmortal = true;
			StartCoroutine(TimerImmortality());
		}


		private IEnumerator TimerImmortality()
		{
			yield return Coroutines.GetWait(_delay);
			isImmortal = false;
		}


		public override void ApplyDamage(float damage)
		{
			if (isImmortal)
				return;
			
			base.ApplyDamage(damage);
		}
	}
}
