using System.Collections.Generic;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.Random;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.Player;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.UI.GamePlay;
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

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		private GameObject HeroGameObject { get; set; }


		public GameFactory(IAssets assets, IStaticDataService staticDataService, IRandomService randomService, IPersistentProgressService progressService)
        {
            _assets = assets;
			_staticDataService = staticDataService;
			_randomService = randomService;
			_progressService = progressService;
		}

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.PLAYER_WITH_SERVICE_PATH, at.transform.position);
			return HeroGameObject;
        }


		public GameObject CreateHud() => 
            _assets.Instantiate(AssetPath.HUD_CANVAS_PATH);


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


		public LootPiece CreateLoot()
		{
			LootPiece lootPiece = InstantiateRegistered(AssetPath.LOOT_GOLD_1_PATH).GetComponent<LootPiece>();
			lootPiece.Constructor(_progressService.Progress.WorldData);
			return lootPiece;
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