using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Utils.Log;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Purchasing;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	public class IAPProvider: IStoreListener
	{
		private const string IAP_CONFIG_PATH = "IAP_Products";
		
		private IStoreController _controller;
		private IExtensionProvider _extension;
		private IAPService _iapService;

		public Dictionary<string, ProductConfig> Configs { get; private set; }
		public Dictionary<string, Product> Products { get; private set; }

		public event Action Initialized;
		public bool IsInitialized => _controller != null && _extension != null;



		public void Initialize( IAPService iapService)
		{
			_iapService = iapService;
			Configs = new Dictionary<string, ProductConfig>();
			Products = new Dictionary<string, Product>();
			
			Load();
			
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			foreach (ProductConfig productConfig in Configs.Values)
				builder.AddProduct(productConfig.Id, productConfig.ProductType);
			
			UnityPurchasing.Initialize(this, builder);
		}

		public void StartPurchase(string productId) =>
			_controller.InitiatePurchase(productId);


		/// <summary>
		/// Только после вызова этого callback покупки будут доступны
		/// </summary>
		/// <param name="controller">содержит информацию  продуктах</param>
		/// <param name="extensions">позволяет реализовать специфические штуки для разных сторов</param>
		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			_controller = controller;
			_extension = extensions;

			foreach (Product product in _controller.products.all)
				Products.Add(product.definition.id, product);

			Initialized?.Invoke();
			
			CustomDebug.Log($"UnityPurchasing initialization success", Color.yellow);
		}


		public void OnInitializeFailed(InitializationFailureReason error) =>
			CustomDebug.Log($"UnityPurchasing OnInitializeFailed: {error}", Color.yellow);
		
		
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
		{
			CustomDebug.Log($"UnityPurchasing ProcessPurchase success{purchaseEvent.purchasedProduct.definition.id}");
			return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
		}


		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
			CustomDebug.Log($"{product.definition.id} purchase FAILED, PurchaseFailureReasons {failureReason}, transaction id {product.transactionID}");


		private void Load()
		{
			//TODO не смог найти метод расширения, надо подумать как обойти.
			// Configs = Resources.Load<TextAsset>(IAP_CONFIG_PATH)
			// 				   .text
			// 				   .AsDeserialized<ProductConfigWrapper>()
			// 				   .Configs.
			// 				   ToDictionary( x => x.Id, x => x);
		}
	}
}
