using System;

namespace InternalAssets.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State PlayerState;
        public WorldData WorldData;
		public Stats HeroStats;

        public PlayerProgress(string initialLevel)
        {
            PlayerState = new State();
            WorldData = new WorldData(initialLevel);
			HeroStats = new Stats();
		}
    }
}