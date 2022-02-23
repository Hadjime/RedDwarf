using InternalAssets.Scripts.Characters.Enemy.EnemySpawners;
using UnityEditor;
using UnityEngine;


namespace InternalAssets.Scripts.Editor
{
	[CustomEditor(typeof(SpawnMarker))]
	public class SpawnMarkerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmoType)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(spawner.transform.position, 0.2f);
			Gizmos.color = Color.white;
		}
	}
}
