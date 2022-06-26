using Cinemachine;
using InternalAssets.Scripts.Characters.Hero;
using UnityEngine;


namespace InternalAssets.Scripts.Weapon
{
	public class CellExplosion : MonoBehaviour
	{
		[SerializeField, Range(0, 10)] private int delay;
		[SerializeField, Range(0, 100)] private int damage;
		[SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;

		void Start() {}

		private void OnTriggerEnter2D(Collider2D other)
		{
			// other.transform.parent.GetComponent<IHealth>()?.ApplyDamage(damage);
			other.GetComponentInParent<IHealth>()?.ApplyDamage(damage);
			_cinemachineImpulseSource.GenerateImpulse(Vector3.one);

			// var objectTile = other.GetComponent<TileSetting>();
			// if (other != null && objectTile != null)
			// {
			//     objectTile.DamageTile(damage);
			// }
		}

		public void DestroyObject()
		{
			Destroy(this.gameObject);
		}
	}
}
