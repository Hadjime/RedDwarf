using System;

namespace InternalAssets.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State PlayerState;
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            PlayerState = new State();
            WorldData = new WorldData(initialLevel);
        }
    }
}