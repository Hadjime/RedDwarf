using System;

namespace InternalAssets.Scripts.Data
{
    [Serializable]
    public class State
    {
        public float CurrentHp;
        public float MaxHp;
		

        public void ResetHp() => 
            CurrentHp = MaxHp;
    }
}