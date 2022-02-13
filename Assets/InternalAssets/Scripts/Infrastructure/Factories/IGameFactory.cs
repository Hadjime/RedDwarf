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
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; }
        event Action HeroCreated;
		void Register(ISavedProgressReader progressReader);
		void Cleanup();
	}
}