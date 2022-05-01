using System;
using AirFishLab.ScrollingList;
using InternalAssets.Scripts.Inventory.Item;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.GamePlay.ScrollingInventoryItems
{
	public class ElementScrolling : ListBox
	{
		[SerializeField] private Image itemIcon;
		[SerializeField] private Button attackBtn; //TODO убрать от сюда
		protected override void UpdateDisplayContent(object content)
		{
			Item itemData = (Item)content;

			itemIcon.sprite = itemData.Icon;
			// attackBtn.interactable = itemData.Amount > 0;


		}
	}
}
