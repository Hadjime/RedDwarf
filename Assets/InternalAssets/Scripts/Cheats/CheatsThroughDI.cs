using System.ComponentModel;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;

namespace InternalAssets.Scripts.Cheats
{
    public class CheatsThroughDI
    {
        private readonly IPersistentProgressService _progressService;
        public static readonly CheatsThroughDI Instance;

        public CheatsThroughDI(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        [Category("LOOT"), DisplayName("Add 100 Gold")]
        public void AddGold()
        {
            _progressService.Progress.WorldData.LootData.Collect(new Loot(){Value = 100});
        }
    }
}