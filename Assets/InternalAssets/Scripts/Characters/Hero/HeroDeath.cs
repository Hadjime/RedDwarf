using System;
using InternalAssets.Scripts.Player;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero
{
    [RequireComponent(typeof(IHealth))]
    [RequireComponent(typeof(HeroMove))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer heroSpriteRenderer;
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private HeroMove heroMove;
		[SerializeField] private HeroAttack heroAttack;
        [SerializeField] private GameObject redStainPrefab;
        
        private bool _isDead = default;


		public event Action HeroDead;

        private void OnValidate()
        {
            heroSpriteRenderer = GetComponent<SpriteRenderer>();
            heroHealth = GetComponent<HeroHealth>();
            heroMove = GetComponent<HeroMove>();
			heroAttack = GetComponent<HeroAttack>();
		}

        private void Start() => 
            heroHealth.HpChanged += OnHpChanged;

        private void OnDestroy() => 
            heroHealth.HpChanged -= OnHpChanged;

        private void OnHpChanged()
        {
            if (!_isDead && heroHealth.CurrentHp <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            heroMove.enabled = false;
			heroAttack.enabled = false;
            heroSpriteRenderer.enabled = false;
            SpawnDeathFx();
			HeroDead?.Invoke();
        }


		private void SpawnDeathFx() =>
			Instantiate(redStainPrefab, transform.position, Quaternion.identity);
	}
}