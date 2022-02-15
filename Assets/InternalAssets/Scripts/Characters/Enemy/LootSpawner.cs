using System;
using InternalAssets.Scripts.Infrastructure.Factories;
using UnityEngine;
using Zenject;


namespace InternalAssets.Scripts.Characters.Enemy
{
	public class LootSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyDeath enemyDeath;
		private IGameFactory _factory;


		public void Constructor(IGameFactory factory)
		{
			_factory = factory;
		}
		
		private void Start()
		{
			enemyDeath.Happened += SpawnLoot;
		}


		private void SpawnLoot()
		{
			GameObject loot = _factory.CreateLoot();
			loot.transform.position = transform.position;
		}
	}
}
