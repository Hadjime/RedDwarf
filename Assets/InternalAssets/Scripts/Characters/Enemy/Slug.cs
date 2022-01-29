using UnityEngine;


namespace InternalAssets.Scripts.Characters.Enemy
{
	public class Slug: Character, IAttacking, IDamageable
	{
		[SerializeField] private int numberDamage = 10;

		private float timer = 1;
		private void OnCollisionStay2D(Collision2D other)
		{
			if (timer > 0)
			{
				timer -= Time.deltaTime;
				return;
			}

			other.gameObject.GetComponent<IDamageable>()?.ApplyDamage(numberDamage);
			timer = 1;
		}
		
		public void ApplyDamage(int amountDamage)
		{
			Destroy(this);
		}
	}
}
