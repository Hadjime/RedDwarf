using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.StaticData;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	public interface IStaticDataService: IService
	{
		void LoadMonsters();
		MonstersStaticData ForMonsters(MonsterTypeId monsterTypeId);
	}
}
