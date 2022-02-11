using System;

namespace InternalAssets.Scripts.Player
{
    public interface IHealth
    {
        float CurrentHp { get; }
        float MaxHp { get; }
        Action HpChanged { get; set; }
        void ApplyDamage(float damage);
    }
}