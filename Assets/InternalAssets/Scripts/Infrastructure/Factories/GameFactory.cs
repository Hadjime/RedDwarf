using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Characters.Enemy.EnemySpawners;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.Map.Grids;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.UI.Elements;
using InternalAssets.Scripts.UI.GamePlay;
using InternalAssets.Scripts.UI.Services.Windows;
using Pathfinding;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;


namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
		private readonly IStaticDataService _staticDataService;
		private IRandomService _randomService;
		private readonly IPersistentProgressService _progressService;
		private IWindowService _windowService;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		private GameObject HeroGameObject { get; set; }


		public GameFactory(
				IAssets assets,
				IStaticDataService staticDataService,
				IRandomService randomService,
				IPersistentProgressService progressService,
				IWindowService windowService
			)
        {
            _assets = assets;
			_staticDataService = staticDataService;
			_randomService = randomService;
			_progressService = progressService;
			_windowService = windowService;
		}


		public async Task WarmUp()
		{
			await _assets.LoadAsync<GameObject>(AssetAddress.LOOT_GOLD_1_PATH);
			await _assets.LoadAsync<GameObject>(AssetAddress.SPAWN_POINT);
		}

		public async Task<GridsManager> CreateGrid(AssetReferenceGameObject grid)
		{
			GameObject gridPrefab = await _assets.LoadAsync<GameObject>(grid);
			GameObject gridGameObject = Object.Instantiate(gridPrefab);
			gridGameObject.TryGetComponent(out GridsManager gridsManager);
			return gridsManager;
		}

		public async Task<GameObject> CreateHero(Vector3 at)
        {
            HeroGameObject = await InstantiateRegisteredAsync(AssetAddress.PLAYER_WITH_SERVICE_PATH, at);
			return HeroGameObject;
        }

		public async Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent)
		{
			MonstersStaticData monstersStaticData = _staticDataService.ForMonsters(typeId);
			
			GameObject prefab = await _assets.LoadAsync<GameObject>(monstersStaticData.PrefabReference);
			
			GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity,
				parent);
			
			IHealth health = monster.GetComponent<IHealth>();
			health.CurrentHp = monstersStaticData.Hp;
			health.MaxHp = monstersStaticData.Hp;

			monster.GetComponent<AgentMoveToPlayer>()?.Constructor(HeroGameObject.transform);

			AILerp aiLerp = monster.GetComponent<AILerp>();
			if (aiLerp != null)
				aiLerp.speed = monstersStaticData.MoveSpeed;
			
			Attack attack = monster.GetComponent<Attack>();
			if (attack != null)
			{
				attack.Constructor(HeroGameObject.transform);
				attack.Damage = monstersStaticData.Damage;
				attack.AttackCooldown = monstersStaticData.AttackCooldown;
				attack.Radius = monstersStaticData.EffectiveRadiusAttack;
			}

			monster.GetComponent<RotateToPlayer>()?.Constructor(HeroGameObject.transform);

			LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
			if (lootSpawner != null)
			{
				lootSpawner.Constructor(this, _randomService);
				lootSpawner.SetLoot(monstersStaticData.MinLoot, monstersStaticData.MaxLoot);
			}

			return monster;
		}


		public async Task<LootPiece> CreateLoot(Transform parent)
		{
			GameObject prefab = await _assets.LoadAsync<GameObject>(AssetAddress.LOOT_GOLD_1_PATH);
			
			LootPiece lootPiece = InstantiateRegistered(prefab, parent.position).GetComponent<LootPiece>();
			if (lootPiece != null)
			{
				lootPiece.Constructor(_progressService.Progress.WorldData);
			}
			
			return lootPiece;
		}


		public async Task CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId)
		{
			GameObject prefab = await _assets.LoadAsync<GameObject>(AssetAddress.SPAWN_POINT);
			
			SpawnPoint spawner = InstantiateRegistered(prefab, at).GetComponent<SpawnPoint>();
			
			spawner.Constructor(this);
			spawner.Id = spawnerId;
			spawner.monsterTypeId = monsterTypeId;
		}


		public void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);
            
			ProgressReaders.Add(progressReader);
		}


		public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
			
			_assets.CleanUp();
        }


		private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await _assets.InstantiateAsync(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }


		[Obsolete]
		private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
		{
			GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}


		private async Task<GameObject> InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = await _assets.InstantiateAsync(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }


		private GameObject InstantiateRegistered(GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate(prefab);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}


		private void RegisterProgressWatchers(GameObject herGameObject)
        {
            foreach (ISavedProgressReader progressReader in herGameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
	}
}