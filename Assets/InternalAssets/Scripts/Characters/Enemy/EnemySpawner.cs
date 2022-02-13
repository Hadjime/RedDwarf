using System;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Enemy
{
	[RequireComponent(typeof(UniqueId))]
	public class EnemySpawner : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private MonsterTypeId monsterTypeId;
		private string _id;
		[SerializeField] private bool _slain;
		private IGameFactory _factory;
		private EnemyDeath _enemyDeath;


		private void Awake()
		{
			_id = GetComponent<UniqueId>()?.Id;
			_factory = AllServices.Container.Single<IGameFactory>();
		}


		public void LoadProgress(PlayerProgress progress)
		{
			if (progress.KillData.ClearedSpawners.Contains(_id))
				_slain = true;
			else
				Spawn();
		}


		private void Spawn()
		{
			GameObject monster = _factory.CreateMonster(monsterTypeId, transform);
			_enemyDeath = monster.GetComponent<EnemyDeath>();
			_enemyDeath.Happened += Kill;
		}


		private void Kill()
		{
			if (_enemyDeath != null)
				_enemyDeath.Happened -= Kill;
			
			_slain = true;
		}


		public void UpdateProgress(PlayerProgress progress)
		{
			if (_slain)
				progress.KillData.ClearedSpawners.Add(_id);
		}
	}
}
