using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.StaticData;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	public interface IStaticDataService: IService
	{
		void Load();
		MonstersStaticData ForMonsters(MonsterTypeId monsterTypeId);
		LevelStaticData ForLevel(string sceneKey);
	}
}
