﻿using System;
using InternalAssets.Scripts.Characters.Enemy;
using UnityEngine;


namespace InternalAssets.Scripts.StaticData
{
	[Serializable]
	public class EnemySpawnerData
	{
		public string Id;
		public MonsterTypeId MonsterTypeId;
		public Vector3 Position;


		public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position)
		{
			Id = id;
			MonsterTypeId = monsterTypeId;
			Position = position;
		}
	}
}