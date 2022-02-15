using System;
using InternalAssets.Scripts.Characters.Enemy;
using UnityEngine;

namespace InternalAssets.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
		public LootData LootData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
			LootData = new LootData();
		}
    }
}