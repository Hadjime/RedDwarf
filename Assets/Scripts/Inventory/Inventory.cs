using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Scriptable Object/New Inventory", order = 51)]
public class Inventory : ScriptableObject
{
    public string namePlayer;
    public int amountMoney;
    public int amountPickAxe;
    public List<InventoryItem> items;
    
}

[System.Serializable]
public class InventoryItem
{
    public string name;
    public Sprite icon;
    public int price;
    public int attackDamage;
    public int protection;
    public int amount;
}


