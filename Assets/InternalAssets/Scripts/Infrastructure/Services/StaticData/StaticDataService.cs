using System.Collections.Generic;
using System.Linq;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.StaticData;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string MonstersLabel = "Monsters";
		
		private readonly IAssets _assets;
		private Dictionary<MonsterTypeId, MonstersStaticData> _monsters;


		public StaticDataService(IAssets assets) {
			_assets = assets;
		}


		public void LoadMonsters()
		{
			_assets.LoadAllAsyncByLabel<MonstersStaticData>(MonstersLabel, onFinish: list =>
			{
				_monsters = list.ToDictionary(data => data.MonsterTypeId, data => data);
			});
		}


		public MonstersStaticData ForMonsters(MonsterTypeId monsterTypeId) =>
			_monsters.TryGetValue(monsterTypeId, out MonstersStaticData staticData) 
				? staticData 
				: null;
	}
}
