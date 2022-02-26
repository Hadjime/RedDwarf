using System.Collections.Generic;
using System.Linq;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.StaticData.Windows;
using InternalAssets.Scripts.UI.Services.Windows;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string MonstersLabel = "Monsters";
		private const string LevelsLabel = "Levels";
		private const string windowStaticDataPath = "WindowStaticData";
		
		private readonly IAssets _assets;
		private Dictionary<MonsterTypeId, MonstersStaticData> _monsters;
		private Dictionary<string, LevelStaticData> _levels;
		private Dictionary<WindowId, WindowConfig> _windowConfigs;


		public StaticDataService(IAssets assets) {
			_assets = assets;
		}


		public void Load()
		{
			_assets.LoadAllAsyncByLabel<MonstersStaticData>(MonstersLabel, onFinish: list =>
			{
				_monsters = list.ToDictionary(data => data.MonsterTypeId, data => data);
			});
			
			_assets.LoadAllAsyncByLabel<LevelStaticData>(LevelsLabel, onFinish: list =>
			{
				_levels = list.ToDictionary(data => data.LevelKey, data => data);
			});

			_windowConfigs = _assets.LoadAsync<WindowStaticData>(windowStaticDataPath)
									.Configs
									.ToDictionary(config => config.WindowId, config => config);
		}


		public MonstersStaticData ForMonsters(MonsterTypeId monsterTypeId) =>
			_monsters.TryGetValue(monsterTypeId, out MonstersStaticData monstersStaticData) 
				? monstersStaticData 
				: null;


		public LevelStaticData ForLevel(string sceneKey) =>
			_levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData)
				? levelStaticData
				: null;


		public WindowConfig ForWindow(WindowId windowId) =>
			_windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
				? windowConfig
				: null;
	}
}
