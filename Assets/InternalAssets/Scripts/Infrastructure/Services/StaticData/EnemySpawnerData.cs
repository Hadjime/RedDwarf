using System;
using InternalAssets.Scripts.Characters.Enemy;
using UnityEngine;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	[Serializable]
	public class EnemySpawnerData
	{
		public string Id;
		public MonsterTypeId MonsterTypeId;
		public Vector3 Position;
	}
}
