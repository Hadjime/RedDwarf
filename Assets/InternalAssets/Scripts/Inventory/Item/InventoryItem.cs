using InternalAssets.Scripts.Inventory.Item;
using UnityEngine;

namespace InternalAssets.Scripts.Inventory
{
    [System.Serializable]
    public class InventoryItem: IItem
    {
        private string name;
        private Sprite icon;
        private int price;
        private int attackDamage;
        private int protection;
        private int amount;
        
        public string Name => name;
        public Sprite Icon => icon;
        public int Price => price;
        public int AttackDamage => attackDamage;
        public int Protection => protection;
        public int Amount
        {
            get => amount;
            set => amount = value;
        }
    }
}