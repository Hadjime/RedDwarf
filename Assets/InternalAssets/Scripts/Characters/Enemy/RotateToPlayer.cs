using System;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services;
using Pathfinding;
using UnityEngine;

namespace InternalAssets.Scripts.Characters.Enemy
{
    public class RotateToPlayer : Seeker
    {
        private IGameFactory _gameFactory;
        private Transform _heroTransform;


        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializedHeroTransform();
            else
                _gameFactory.HeroCreated += OnHeroCreated;
        }

        private void Update()
        {
            if (Initialized())
            {
                LookAtImmediately(_heroTransform.position);
            }
        }

        private void LookAtImmediately(Vector3 heroTransformPosition)
        {
            Vector3 direction = heroTransformPosition - transform.position;
            direction = Quaternion.Euler(0, 0, 90) * direction;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        private bool Initialized() => 
            _heroTransform != null;

        private void InitializedHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private void OnHeroCreated() => 
            InitializedHeroTransform();
    }
}