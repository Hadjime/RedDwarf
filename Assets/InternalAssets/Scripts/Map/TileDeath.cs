using System;
using UnityEngine;


namespace InternalAssets.Scripts.Map
{
	[RequireComponent(typeof(TileHealth))]
	public class TileDeath: MonoBehaviour
	{
		[SerializeField] private TileHealth tileHealth;


		private void Start()
		{
			tileHealth.HpChanged += hOnHpChanged;
		}


		private void OnDestroy()
		{
			tileHealth.HpChanged -= hOnHpChanged;
		}


		private void hOnHpChanged()
		{
			if (tileHealth.CurrentHp <= 0)
				Die();
		}


		private void Die()
		{
			Destroy(gameObject);
		}
	}
}
