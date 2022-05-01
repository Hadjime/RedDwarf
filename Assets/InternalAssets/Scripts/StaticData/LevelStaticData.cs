using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace InternalAssets.Scripts.StaticData
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
	public class LevelStaticData: ScriptableObject
	{
		public string LevelKey;
		public AssetReferenceGameObject Grid;
		public List<EnemySpawnerData> EnemySpawners;
		public Vector3 InitialHeroPosition;
	}
}
