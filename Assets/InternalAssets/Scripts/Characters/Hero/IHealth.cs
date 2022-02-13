using System;


namespace InternalAssets.Scripts.Characters.Hero
{
    public interface IHealth
    {
        float CurrentHp { get; set; }
        float MaxHp { get; set; }
		event Action HpChanged;
        void ApplyDamage(float damage);
    }
}