using UnityEngine.Purchasing;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	public class ProductDescription
	{
		public string Id;
		public Product Product;
		public ProductConfig Config;
		public int AvailablePurchasesLeft;
	}
}
