using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.StaticData;
using InternalAssets.Scripts.StaticData.Windows;
using InternalAssets.Scripts.UI.Services.Windows;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	public interface IStaticDataService: IService
	{
		void Load();
		MonstersStaticData ForMonsters(MonsterTypeId monsterTypeId);
		LevelStaticData ForLevel(string sceneKey);
		WindowConfig ForWindow(WindowId shop);
	}
}
