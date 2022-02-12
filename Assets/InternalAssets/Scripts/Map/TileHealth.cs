using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Player;
using UnityEngine;


namespace InternalAssets.Scripts.Map
{
	public class TileHealth : MonoBehaviour, IHealth
	{
		[SerializeField] private float currentHp;
		[SerializeField] private float maxHp;
		[SerializeField] private SpriteRenderer currentSprite;
		public List<Sprite> listSpritesForVisualisationHealth;
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
			SetSprite(currentHp);
			HpChanged?.Invoke();
		}

		private void SetSprite(float hp)
		{
			if (listSpritesForVisualisationHealth == null)
				return;
			
			if (hp > MaxHp * 0.7f && hp <= MaxHp)
			{
				currentSprite.sprite = listSpritesForVisualisationHealth[0];
			}
			if (hp > MaxHp * 0.3f && hp < MaxHp * 0.7f)
			{
				currentSprite.sprite = listSpritesForVisualisationHealth[1];
			}
			if (hp >= 0 && hp < MaxHp * 0.3f)
			{
				currentSprite.sprite = listSpritesForVisualisationHealth[2];
			}
		}
	}
}
