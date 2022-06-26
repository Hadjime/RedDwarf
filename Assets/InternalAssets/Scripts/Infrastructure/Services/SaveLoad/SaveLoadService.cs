using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Factories;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService persistentProgressService, IGameFactory gameFactory)
        {
            _persistentProgressService = persistentProgressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_persistentProgressService.Progress);
            }
            
            PlayerPrefs.SetString(ProgressKey, _persistentProgressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}