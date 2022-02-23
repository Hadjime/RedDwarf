using System.Linq;
using InternalAssets.Scripts.Characters;
using InternalAssets.Scripts.Characters.Enemy.EnemySpawners;
using InternalAssets.Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Editor
{
	[CustomEditor(typeof(LevelStaticData))]
	public class LevelStaticDataEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			LevelStaticData levelData = (LevelStaticData)target;

			if (GUILayout.Button("Collect"))
			{
				levelData.EnemySpawners = 
					FindObjectsOfType<SpawnMarker>()
						.Select(marker => new EnemySpawnerData(marker.GetComponent<UniqueId>().Id, marker.MonsterTypeId, marker.transform.position))
						.ToList();

				levelData.LevelKey = SceneManager.GetActiveScene().name;
			}
			
			EditorUtility.SetDirty(target);
		}
	}
}
