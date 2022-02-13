using System;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Player;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Enemy
{
	public class EnemyHealth : MonoBehaviour, IHealth
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
			//TODO сюда и анимацию попадания надо будет сделать потом animaor.Play("HitAnimation")
		}
	}
}
