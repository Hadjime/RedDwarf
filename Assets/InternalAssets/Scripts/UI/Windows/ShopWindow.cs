using System;
using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Windows
{
	class ShopWindow : WindowBase
	{
		[SerializeField] private TextMeshProUGUI goldText;


		protected override void Initialize() =>
			UpdateGoldText();
		
		protected override void SubscribeUpdates() =>
			Progress.WorldData.LootData.Changed += UpdateGoldText;
		
		protected override void Cleanup()
		{
			base.Cleanup();
			Progress.WorldData.LootData.Changed -= UpdateGoldText;
		}
		
		private void UpdateGoldText() =>
			goldText.text = Progress.WorldData.LootData.Collected.ToString();
	}
}
