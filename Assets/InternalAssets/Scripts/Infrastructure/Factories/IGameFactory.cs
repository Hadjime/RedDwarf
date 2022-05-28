﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Map.Grids;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateHero(Vector3 at);
		List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
		GameObject HeroGameObject { get; }

		Task<GameObject> CreateMonster(MonsterTypeId typeId, Transform parent);
		Task CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId);
		Task<LootPiece> CreateLoot(Transform parent);
		void Cleanup();


		Task WarmUp();

		Task<GridsManager> CreateGrid(AssetReferenceGameObject grid);
	}	
}