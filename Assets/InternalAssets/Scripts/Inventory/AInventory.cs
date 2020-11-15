using System.Collections.Generic;
using InternalAssets.Scripts.Inventory.Item;
using UnityEngine;

namespace InternalAssets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New AInventory", menuName = "Scriptable Object/New AInventory", order = 51)]
    public class AInventory : ScriptableObject, IInventory
    {
        [SerializeField] private string namePlayer;
        [SerializeField] private int amountHp;
        [SerializeField] private int amountMoney;
        [SerializeField] private int amountPickAxe;
        [SerializeField] private List<Item.Item> items;

        public string NamePlayer { get; set; }
        public int AmountHp
        {
            get => amountHp;
            set
            {
                amountHp = value <= 0 ? 0 : value;
                EventManager.StartEvent("OnHPChange");
            }
        }
        public int AmountMoney
        {
            get => amountMoney;
            set
            {
                amountMoney = value;
                EventManager.StartEvent("OnMoneyChange");
            }
        }
        public int AmountPickAxe
        {
            get => amountPickAxe;
            set
            {
                amountPickAxe = value;
                EventManager.StartEvent("OnPickAxeChange");
            }
        }
        public List<Item.Item> Items
        {
            get => items;
            set => items = value;
        }

        public void ApplyDamage(int damage)
        {
            AmountHp -= damage;
        }

        public void AddCoin(int coin)
        {
            AmountMoney += coin;
        }

        public void AddPickAxe(int number)
        {
            AmountPickAxe += number;
        }
    }
}
