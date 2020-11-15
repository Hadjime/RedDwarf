﻿using UnityEngine;

namespace InternalAssets.Scripts.Inventory.Item
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item/New Item", order = 51)]
    public class Item : ScriptableObject, IItem
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private int price;
        [SerializeField] private int attackDamage;
        [SerializeField] private int protection;
        [SerializeField] private int amount;
        [SerializeField] private GameObject prefab;

        public string Name => name;
        public Sprite Icon => icon;
        public int Price => price;
        public int AttackDamage => attackDamage;
        public int Protection => protection;
        public int Amount
        {
            get => amount;
            set
            {
                if (amount <= 0)
                {
                    amount = 0;
                }
                else
                {
                    amount = value;
                }
                EventManager.StartEvent("OnItemChange");
            }
        }

        public GameObject Prefab => prefab;
    }
}
