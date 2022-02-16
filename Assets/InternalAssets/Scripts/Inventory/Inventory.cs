using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets.Scripts.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Scriptable Object/New Inventory", order = 51)]
    public class Inventory : UnityEngine.ScriptableObject, IInventory
    {
        [SerializeField] private string namePlayer;
        [SerializeField] private int amountHp;
        [SerializeField] private int amountMoney;
        [SerializeField] private int amountPickAxe;
        [SerializeField] private List<Item.Item> items;
        [SerializeField] private Item.Item currentWeapon;
        
        public Item.Item CurrentWeapon { get => currentWeapon; }
        
        public string NamePlayer { get; set; }

        // private void OnEnable()
        // {
        //     EventManager.StartListeningWithOneParametr("ChangingCurrentWeapon", HandleChangingCurrentWeapon);
        // }
        //
        // private void OnDisable()
        // {
        //     EventManager.StopListeningWithOneParametr("ChangingCurrentWeapon", HandleChangingCurrentWeapon);
        // }

        private void HandleChangingCurrentWeapon(int weaponID)
        {
            currentWeapon = items[weaponID];
        }
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