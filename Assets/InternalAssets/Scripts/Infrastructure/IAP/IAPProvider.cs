using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Utils.Log;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Purchasing;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	public partial class IAPProvider: IStoreListener
	{
		private const string IAP_CONFIG_PATH = "IAP_Products";
		
		private IStoreController _controller;
		private IExtensionProvider _extension;

		public Dictionary<string, ProductConfig> Configs { get; private set; }

		public event Action Initialized;
		public bool IsInitialized => _controller != null && _extension != null;



		public void Initialize()
		{
			Configs = new Dictionary<string, ProductConfig>();
			
			Load();
			
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			foreach (ProductConfig productConfig in Configs.Values)
				builder.AddProduct(productConfig.Id, productConfig.ProductType);
			
			UnityPurchasing.Initialize(this, builder);
		}


		public void StartPurchase(string productId) =>
			_controller.InitiatePurchase(productId);


		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			_controller = controller;
			_extension = extensions;
			
			Initialized?.Invoke();
			
			CustomDebug.Log($"UnityPurchasing initialization success", Color.yellow);
		}


		public void OnInitializeFailed(InitializationFailureReason error) =>
			CustomDebug.Log($"UnityPurchasing OnInitializeFailed: {error}", Color.yellow);


		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
		{
			CustomDebug.Log($"UnityPurchasing ProcessPurchase success{purchaseEvent.purchasedProduct.definition.id}");
			return PurchaseProcessingResult.Complete;
		}


		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
			CustomDebug.Log($"{product.definition.id} purchase FAILED, PurchaseFailureReasons {failureReason}, transaction id {product.transactionID}");


		private void Load()
		{
			Configs = Resources.Load<TextAsset>(IAP_CONFIG_PATH).text.AsDeserialized<ProductConfigWrapper>().Configs;
		}
	}
}
