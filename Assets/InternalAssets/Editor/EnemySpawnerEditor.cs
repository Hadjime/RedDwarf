﻿using InternalAssets.Scripts.Characters.Enemy;
using UnityEditor;
using UnityEngine;


namespace InternalAssets.Editor
{
	[CustomEditor(typeof(EnemySpawner))]
	public class EnemySpawnerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmoType)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(spawner.transform.position, 0.5f);
			Gizmos.color = Color.white;
		}
	}
}
