using System.ComponentModel;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;
using InternalAssets.Scripts.Map.Grids;
using UnityEngine;


namespace InternalAssets.Scripts.Cheats
{
    public class CheatsThroughDI
    {
        private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadService;
		public static readonly CheatsThroughDI Instance;

        public CheatsThroughDI(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
		{
			_progressService = progressService;
			_saveLoadService = saveLoadService;
		}

        [Category("LOOT"), DisplayName("Add 100 Gold")]
        public void AddGold()
        {
            _progressService.Progress.WorldData.LootData.Collect(new Loot(){Value = 100});
			_saveLoadService.SaveProgress();
        }

		[Category("GamePlay"), DisplayName("Fog of war")]
		public bool IsFogOfWar
		{
			get
			{
				GridsManager gridsManager = Object.FindObjectOfType<GridsManager>();
				return gridsManager.IsActiveFogOfWar;
			}
			set
			{
				GridsManager gridsManager = Object.FindObjectOfType<GridsManager>();
				gridsManager.SetActiveFogOfWar(value);
			}
		}
	}
}