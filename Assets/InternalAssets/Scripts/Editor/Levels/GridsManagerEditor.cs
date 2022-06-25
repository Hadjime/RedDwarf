using System.Linq;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Map.Grids;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace InternalAssets.Scripts.Editor.Levels
{
	[CustomEditor(typeof(GridsManager))]
	public class GridsManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GridsManager gridsManager = (GridsManager)target;
			
			if (GUILayout.Button("SearchAllLootInLevel"))
			{
				gridsManager.LootsInLevel = StageUtility.GetCurrentStageHandle()
														.FindComponentsOfType<LootPiece>()
														.ToList();
			}
		}
	}
}
