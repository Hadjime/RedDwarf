using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory_2", menuName = "Scriptable Object/New Inventory_2", order = 51)]
public class Inventory_2 : ScriptableObject
{
    public List<InventoryItem_2> items;
}

[System.Serializable]
public class InventoryItem_2
{
    public string name;
    public Sprite icon;
    public int price;
    public int attackDamage;
}


