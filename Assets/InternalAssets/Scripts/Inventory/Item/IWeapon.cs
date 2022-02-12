using UnityEngine;

namespace InternalAssets.Scripts.Inventory.Item
{
    public interface IWeapon
    {
        void PushObject(Vector2 direction);
    }
}