using System;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using UnityEngine;
using Zenject;


namespace InternalAssets.Scripts.Characters.Enemy
{
	public class LootSpawner : MonoBehaviour
	{
		[SerializeField] private EnemyDeath enemyDeath;
		private IGameFactory _factory;
		private IRandomService _random;
		private int _lootMin;
		private int _lootMax;


		public void Constructor(IGameFactory factory, IRandomService random)
		{
			_factory = factory;
			_random = random;
		}
		
		private void Start()
		{
			enemyDeath.Happened += SpawnLoot;
		}


		private void SpawnLoot()
		{
			LootPiece loot = _factory.CreateLoot();
			loot.transform.position = transform.position;

			var lootItem = GenerateLoot();
			loot.Initialize(lootItem);
		}


		public void SetLoot(int min, int max)
		{
			_lootMin = min;
			_lootMax = max;
		}


		private Loot GenerateLoot() =>
			new Loot()
			{
				Value = _random.Next(_lootMin, _lootMax)
			};
	}
}
