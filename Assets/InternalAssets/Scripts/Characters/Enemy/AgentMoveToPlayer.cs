using System;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services;
using Pathfinding;
using UnityEngine;

namespace InternalAssets.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(AIDestinationSetter))]
    public class AgentMoveToPlayer : Seeker
    {
        [SerializeField] private AIDestinationSetter aiDestinationSetter;
        private IGameFactory _gameFactory;
        private Transform _heroTransform;

        private void Reset()
        {
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void OnEnable() => 
            aiDestinationSetter.enabled = true;

        private void OnDisable() => 
            aiDestinationSetter.enabled = false;
		

		public void Constructor(Transform heroTransform)
		{
			_heroTransform = heroTransform;
			
			InitializedHeroTransform();
		}


		private void InitializedHeroTransform() =>
			aiDestinationSetter.target = _heroTransform;
	}
}