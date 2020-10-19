using System.Collections.Generic;
using InternalAssets.Scripts.Inventory.Item;

namespace InternalAssets.Scripts.Inventory
{
    public interface IInventory
    {
        string NamePlayer { get; set; }
        int AmountHp{ get; set; }
        int AmountMoney{ get; set; }
        int AmountPickAxe{ get; set; }
        List<Item.Item> Items { get; set; }
    }
}