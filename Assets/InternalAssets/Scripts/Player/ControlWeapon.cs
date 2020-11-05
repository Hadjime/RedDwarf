using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Inventory.Item;
using UnityEngine;

namespace InternalAssets.Scripts.Player
{
    public class ControlWeapon : SnapGrid
    {
        [SerializeField] private Inventory.Inventory inventory;
        [SerializeField] private Item currentWeapon;
        private Action AttackAction;

        private void Start()
        {
            AttackAction = AttackSmallBomb;
        }

        private void OnEnable()
        {
            EventManager.StartListeningWithOneParametr("ChangingCurrentWeapon", HandleChangingCurrentWeapon);
        }

        private void OnDisable()
        {
            EventManager.StopListeningWithOneParametr("ChangingCurrentWeapon", HandleChangingCurrentWeapon);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                AttackAction();
            }
        }

        private void HandleChangingCurrentWeapon(int weaponID)
        {
            currentWeapon = inventory.Items[weaponID];
        }
        
        public void AttackSmallBomb()
        {
            inventory.Items[0].Amount -= 1; 
            Instantiate(inventory.Items[0].Prefab, SnapPos(), Quaternion.identity);
        }
    }
}