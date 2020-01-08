using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Scriptable Object/New Inventory", order = 51)]
public class Inventory : ScriptableObject
{
    public string namePlayer;
    [SerializeField]
    private int amountHp;
    public int AmountHp
    {
        get => amountHp;
        set
        {
            amountHp = value <= 0 ? 0 : value;
            EventManager.TriggerEvent("HPChange");
        }
    }

    [SerializeField]
    private int amountMoney;
    public int AmountMoney
    {
        get => amountMoney;
        set
        {
            amountMoney = value;
            EventManager.TriggerEvent("MoneyChange");
        }
    }
    private int amountPickAxe;
    public int AmountPickAxe
    {
        get => amountPickAxe;
        set
        {
            amountPickAxe = value;
            EventManager.TriggerEvent("PickAxeChange");
        }
    }
    public List<InventoryItem> items;

    public void ApplyDamage(int damage)
    {
        AmountHp -= damage;
    }
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


