using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.Input;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Inventory.Item;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero
{
	public class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		[SerializeField] private GameObject currentWeaponPrefab;
		private const string LayerName = "Hittable";
		private IInputService _input;
		private int _layerMask;
		private Collider2D[] _hits = new Collider2D[20];
		private Stats _stats;


		private void Awake()
		{
			_input = AllServices.Container.Single<IInputService>();
			_input.IsAttackBtnUp += OnIsAttackBtnUp;
			_layerMask = 1 << LayerMask.NameToLayer(LayerName);
		}


		private void OnDestroy()
		{
			_input.IsAttackBtnUp -= OnIsAttackBtnUp;
		}


		public void LoadProgress(PlayerProgress progress) =>
			_stats = progress.HeroStats;


		public void SetCurrentWeapon(Item item) =>
			currentWeaponPrefab = item.Prefab;


		private void OnIsAttackBtnUp()
		{
			SpawnBomb();
			// int count = Hit();
			// for (int i = 0; i < count; i++)
			// {
			// 	_hits[i].transform.parent.GetComponent<IHealth>().ApplyDamage(_stats.Damage);
			// }
			CustomDebug.Log($"Hero attack", Color.magenta);
		}


		private void SpawnBomb() { Instantiate(currentWeaponPrefab, transform.position.SnapPosition(), Quaternion.identity); }


		private int Hit() =>
			Physics2D.OverlapCircleNonAlloc(transform.position, _stats.DamageRadius, _hits, _layerMask);
		
	}
}
