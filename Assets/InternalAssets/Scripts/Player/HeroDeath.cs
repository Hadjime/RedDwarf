using System;
using InternalAssets.Scripts.Infrastructure;
using UnityEngine;

namespace InternalAssets.Scripts.Player
{
    [RequireComponent(typeof(IHealth))]
    [RequireComponent(typeof(HeroMove))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer heroSpriteRenderer;
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private HeroMove heroMove;
        [SerializeField] private GameObject redStainPrefab;
        
        private bool _isDead = default;

        private void OnValidate()
        {
            heroSpriteRenderer = GetComponent<SpriteRenderer>();
            heroHealth = GetComponent<HeroHealth>();
            heroMove = GetComponent<HeroMove>();
        }

        private void Start() => 
            heroHealth.HpChanged += OnHpChanged;

        private void OnDestroy() => 
            heroHealth.HpChanged -= OnHpChanged;

        private void OnHpChanged()
        {
            if (heroHealth.CurrentHp <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            heroMove.enabled = false;
            heroSpriteRenderer.enabled = false;
            Instantiate(redStainPrefab, transform.position, Quaternion.identity);
        }
    }
}