using System.ComponentModel;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;


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
    }
}