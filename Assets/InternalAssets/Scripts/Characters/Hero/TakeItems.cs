using System;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Map;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero
{
	[Obsolete]
    public class TakeItems : MonoBehaviour, IDamageable
    {
        public Inventory.Inventory inventory;
        public void OnTriggerEnter2D(Collider2D other)
        {

            var item = other.GetComponent<ISelect>();
            if (item != null)
            {
                inventory.AmountMoney += item.GetItem();
            }
        }


		public void ApplyDamage(int amountDamage) =>
			inventory.AmountHp -= amountDamage;
	}
}
