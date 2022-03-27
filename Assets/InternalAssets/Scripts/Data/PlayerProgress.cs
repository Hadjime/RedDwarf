using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Data.PlayerResources;


namespace InternalAssets.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State PlayerState;
        public WorldData WorldData;
		public Stats HeroStats;
		public KillData KillData;
		public ResourceData ResourceData;
		public PurchaseData PurchasesData;


		public PlayerProgress(string initialLevel)
        {
            PlayerState = new State();
            WorldData = new WorldData(initialLevel);
			HeroStats = new Stats();
			KillData = new KillData();
			ResourceData = new ResourceData();
			PurchasesData = new PurchaseData();
		}
	}
}