﻿using InternalAssets.Scripts.Characters.Enemy;
using UnityEditor;
using UnityEngine;


namespace InternalAssets.Scripts.Editor
{
	[CustomEditor(typeof(EnemySpawner))]
	public class EnemySpawnerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(EnemySpawner spawner, GizmoType gizmoType)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(spawner.transform.position, 0.2f);
			Gizmos.color = Color.white;
		}
	}
}