using System;
using AirFishLab.ScrollingList;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Inventory.Item;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.GamePlay
{
    public class GamePlayPanel : WindowBase
    {
        [SerializeField] private HealthBar healthBar;
		[SerializeField] private CircularScrollingList circularScrollingList;
		[SerializeField] private Button attackBtn;


        private IHealth _heroHealth;
		private HeroAttack _heroAttack;
		private BaseListBank _listBank;


		public void Constructor(IHealth heroHealth, HeroAttack heroAttack)
        {
			_heroAttack = heroAttack;
			_heroHealth = heroHealth;
            _heroHealth.HpChanged += OnUpdateHealthBar;
			circularScrollingList.setting.onMovementEnd.AddListener(OnWeaponChanged);
			attackBtn.onClick.AddListener(OnAttack);
		}


		private void OnWeaponChanged()
		{
			_listBank ??= circularScrollingList.listBank;
			int contentID = circularScrollingList.GetCenteredContentID();
			Item item = (Item)_listBank.GetListContent(contentID);
			_heroAttack.SetCurrentWeapon(item);
		}


		private void OnDestroy()
		{
			_heroHealth.HpChanged -= OnUpdateHealthBar;
			attackBtn.onClick.RemoveListener(OnAttack);
		}


		private void OnUpdateHealthBar() => 
            healthBar.SetValue(_heroHealth.CurrentHp, _heroHealth.MaxHp);


		private void OnAttack()
		{
			int centeredContentID = circularScrollingList.GetCenteredContentID();
			CustomDebug.Log($"Attack ID element = {centeredContentID}", Color.green);
		}
	}
}