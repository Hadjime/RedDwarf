using System.Threading.Tasks;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Enemy.EnemySpawners
{
	public class SpawnPoint : MonoBehaviour, ISavedProgress
	{
		public MonsterTypeId monsterTypeId;
		[SerializeField] private bool _slain; //что бы было видно в инспекторе
		private EnemyDeath _enemyDeath;
		private IGameFactory _gameFactory;

		public string Id { get; set; }


		public void Constructor(IGameFactory gameFactory) =>
			_gameFactory = gameFactory;


		public void LoadProgress(PlayerProgress progress)
		{
			if (progress.KillData.ClearedSpawners.Contains(Id))
				_slain = true;
			else
				Spawn();
		}


		public void UpdateProgress(PlayerProgress progress)
		{
			if (_slain)
				progress.KillData.ClearedSpawners.Add(Id);
		}

		private async void Spawn()
		{
			GameObject monster = await _gameFactory.CreateMonster(monsterTypeId, transform);
			_enemyDeath = monster.GetComponent<EnemyDeath>();
			_enemyDeath.Happened += Kill;
		}


		private void Kill()
		{
			if (_enemyDeath != null)
				_enemyDeath.Happened -= Kill;
			
			_slain = true;
		}
	}
}
