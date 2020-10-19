﻿using UnityEngine;

namespace InternalAssets.Scripts.Inventory.Item
{
    public interface IItem
    {
        string Name { get; }
        Sprite Icon { get; }
        int Price { get; }
        int AttackDamage { get; }
        int Protection { get; }
        int Amount { get; set; }
    }
}