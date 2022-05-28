using System.Collections.Generic;
using System.Linq;
using InternalAssets.Scripts.Characters;
using InternalAssets.Scripts.Characters.Enemy.EnemySpawners;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.StaticData;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Editor
{
	[CustomEditor(typeof(LevelStaticData))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		private const string INITIAL_POINT_TAG = "InitialPoint";
		
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LevelStaticData levelData = (LevelStaticData)target;

			if (GUILayout.Button("Collect"))
			{
				levelData.EnemySpawners = StageUtility.GetCurrentStageHandle()
													  .FindComponentsOfType<SpawnMarker>()
													  .Select(marker =>
														  new EnemySpawnerData(marker.GetComponent<UniqueId>().Id, marker.MonsterTypeId, marker.transform.position))
													  .ToList();

				// levelData.EnemySpawners = 
				// 	FindObjectsOfType<SpawnMarker>()
				// 		.Select(marker => new EnemySpawnerData(marker.GetComponent<UniqueId>().Id, marker.MonsterTypeId, marker.transform.position))
				// 		.ToList();

				levelData.LevelKey = SceneManager.GetActiveScene().name;

				levelData.InitialHeroPosition = StageUtility.GetCurrentStageHandle().FindComponentOfType<HeroInitialPointMarker>().transform.position;
			}
			
			EditorUtility.SetDirty(target);
		}
	}
}
