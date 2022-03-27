using System;
using InternalAssets.Scripts.Data.PlayerResources;
using UnityEngine.Purchasing;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	[Serializable]
	public class ProductConfig
	{
		public string Id;
		public ProductType ProductType;
		public int MaxPurchaseCount;
		public ResourceType ResourceType;
		public int Quantity;
	}
}
