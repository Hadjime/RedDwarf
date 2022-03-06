using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 at);
		[Obsolete] GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
		
		GameObject CreateMonster(MonsterTypeId typeId, Transform parent);
		void CreateSpawner(string spawnerId, Vector3 at, MonsterTypeId monsterTypeId);
		LootPiece CreateLoot(Transform parent);
		void Cleanup();
	}	
}