using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Player;
using InternalAssets.Scripts.UI.GamePlay;
using UnityEngine;


namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject HeroGameObject { get; private set; }
        public event Action HeroCreated;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.PLAYER_WITH_SERVICE_PATH, at.transform.position);
            HeroCreated?.Invoke();
            return HeroGameObject;
        }

        public GameObject CreateHud() => 
            _assets.Instantiate(AssetPath.HUD_CANVAS_PATH);


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