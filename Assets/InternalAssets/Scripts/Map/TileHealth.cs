using System;
using InternalAssets.Scripts.Player;
using UnityEngine;


namespace InternalAssets.Scripts.Map
{
	public class TileHealth : MonoBehaviour, IHealth
	{
		[SerializeField] private float currentHp;
		[SerializeField] private float maxHp;
		public float CurrentHp
		{
			get => currentHp;
			set => currentHp = value;
		}
		public float MaxHp
		{
			get => maxHp;
			set => maxHp = value;
		}
		public event Action HpChanged;


		public void ApplyDamage(float damage)
		{
			CurrentHp -= damage;
			HpChanged?.Invoke();
		}
	}
}
