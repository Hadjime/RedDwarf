using System.Collections.Generic;
using UnityEngine;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
	public class LevelStaticData: ScriptableObject
	{
		public string LevelKey;
		public List<EnemySpawnerData> enemySpawners;
	}
}
