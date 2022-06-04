using System;
using AirFishLab.ScrollingList;
using InternalAssets.Scripts.Inventory.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.GamePlay.ScrollingInventoryItems
{
	public class ElementScrolling : ListBox
	{
		[SerializeField] private Image backLight;
		[SerializeField] private GameObject itemNotAvailable;
		[SerializeField] private Image itemIcon;
		[SerializeField] private TextMeshProUGUI amountTMP;
		[SerializeField] private Button attackBtn; //TODO убрать от сюда

		public Item ItemData { get; private set; }

		private void OnEnable() =>
			SetBackLight(false);

		protected override void UpdateDisplayContent(object content)
		{
			ItemData = (Item)content;

			itemIcon.sprite = ItemData.Icon;
			amountTMP.text = ItemData.Amount.ToString();
			// attackBtn.interactable = itemData.Amount > 0;
			bool IsAvaliable = ItemData.Amount <= 0;
			SetAvailable(IsAvaliable);
		}

		public void SetAvailable(bool isState) =>
			itemNotAvailable.SetActive(isState);

		public void SetBackLight(bool isState) =>
			backLight.gameObject.SetActive(isState);
	}
}
