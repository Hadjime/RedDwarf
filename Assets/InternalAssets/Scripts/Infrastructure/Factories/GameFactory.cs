using System.Collections.Generic;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Characters.Enemy.EnemySpawners;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.UI.Elements;
using InternalAssets.Scripts.UI.GamePlay;
using InternalAssets.Scripts.UI.Services.Windows;
using Pathfinding;
using UnityEngine;
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

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.PLAYER_WITH_SERVICE_PATH, at.transform.position);
			return HeroGameObject;
        }


		public GameObject CreateHud()
		{
			GameObject hud = _assets.Instantiate(AssetPath.HUD_CANVAS_PATH);
			hud.GetComponentInChildren<LootCounter>()?.Constructor(_progressService.Progress.WorldData);

			foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
			{
				openWindowButton.Constructor(_windowService);
			}
			return hud;
		}


		public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
		{
			MonstersStaticData monstersStaticData = _staticDataService.ForMonsters(typeId);
			GameObject monster = Object.Instantiate(monstersStaticData.Prefab, parent.position, Quaternion.identity,
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


		public LootPiece CreateLoot(Transform parent)
		{
			LootPiece lootPiece = InstantiateRegistered(AssetPath.LOOT_GOLD_1_PATH, parent.position).GetComponent<LootPiece>();
			if (lootPiece != null)
			{
				lootPiece.Constructor(_progressService.Progress.WorldData);
			}
			
			return lootPiece;
		}


		public void CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId)
		{
			SpawnPoint spawner = InstantiateRegistered(AssetPath.SPAWN_POINT, at)
				.GetComponent<SpawnPoint>();
			
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
        }


		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }


		private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
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