using System;
using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Windows.Shop
{
	class ShopWindow : WindowBase
	{
		[SerializeField] private TextMeshProUGUI goldText;
		[SerializeField] private RewardedAdItem AdItem;


		protected override void Initialize()
		{
			//AdItem.Initialize(); // TODO надо разобраться с 4 версией unity ads
			UpdateGoldText();
		}


		protected override void SubscribeUpdates()
		{
			// AdItem.Subscribe(); // TODO надо разобраться с 4 версией unity ads
			Progress.WorldData.LootData.Changed += UpdateGoldText;
		}


		protected override void Cleanup()
		{
			base.Cleanup();
			// AdItem.Cleanup(); //TODO надо разобраться с 4 версией unity ads
			Progress.WorldData.LootData.Changed -= UpdateGoldText;
		}
		
		private void UpdateGoldText() =>
			goldText.text = Progress.WorldData.LootData.Collected.ToString();
	}
}
