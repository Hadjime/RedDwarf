using System;
using InternalAssets.Scripts.Inventory.Item;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero
{
	public class ControlWeapon : SnapGrid
	{
		[SerializeField] private Inventory.Inventory inventory;
		[SerializeField] private Item currentWeapon;
		[SerializeField] private int currentIdWeapon;
		private Action<int> AttackAction;

		private void Start()
		{
			AttackAction = AttackCurrentWeapon;
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
			if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))
			{
				AttackAction(currentIdWeapon);
			}
		}

		private void HandleChangingCurrentWeapon(int weaponID)
		{
			currentWeapon = inventory.Items[weaponID];
			currentIdWeapon = weaponID;
		}

		public void AttackCurrentWeapon(int weaponID)
		{
			if (inventory.Items[weaponID].Amount <= 0) return;

			inventory.Items[weaponID].Amount -= 1;
			Instantiate(inventory.Items[weaponID].Prefab, SnapPos(), Quaternion.identity);
		}
	}
}
