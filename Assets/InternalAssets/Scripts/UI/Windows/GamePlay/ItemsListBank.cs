using System;
using AirFishLab.ScrollingList;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Windows.GamePlay
{
	public class ItemsListBank : BaseListBank
	{
		[SerializeField] private Inventory.Inventory inventoryConfig;
			
		public override object GetListContent(int index)
		{
			return inventoryConfig.Items[index];
		}


		public override int GetListLength()
		{
			return inventoryConfig.Items.Count;
		}
	}
}
