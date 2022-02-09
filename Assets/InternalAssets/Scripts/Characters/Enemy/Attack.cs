using System;
using System.Linq;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;

namespace InternalAssets.Scripts.Characters.Enemy
{
    public class Attack : MonoBehaviour
    {
        private const float Radius = 0.4f;
        
        
        [SerializeField] private float attackCooldown = 1f;
        private IGameFactory _gameFactory;
        private Transform _heroTransform;
        private float _attackCooldown = default;
        private int _layerMask;
        private readonly Collider2D[] _hit = new Collider2D[10];
        private bool _attackIsActive = default;

        public bool isAttacking { get; private set; }

        private void Awake()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _gameFactory.HeroCreated += OnHeroCreated;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
            
            if (CanAttack())
                StartAttack();
        }

        public void EnableAttack() => 
            _attackIsActive = true;

        public void DisableAttack() => 
            _attackIsActive = false;

        private bool CooldownIsUp() => 
            _attackCooldown <= 0;

        private bool CanAttack() => 
            _attackIsActive && !isAttacking && CooldownIsUp();

        private void StartAttack()
        {
            isAttacking = true;
            _attackCooldown = attackCooldown;

            if (Hit(out Collider2D hit))
            {
                PhysicsDebug.DrawDebug(hit.transform.position, 1, 1);
                CustomDebug.Log($"hit = {hit.name}", Color.magenta);
            }

            isAttacking = false;
        }

        private bool Hit(out Collider2D hit)
        {
            PhysicsDebug.DrawDebug(transform.position, 1, 1);
            int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, Radius, _hit, _layerMask);
            hit = _hit.FirstOrDefault();
            return hitCount > 0;
        }

        private void OnHeroCreated() => 
            _heroTransform = _gameFactory.HeroGameObject.transform;
    }
}