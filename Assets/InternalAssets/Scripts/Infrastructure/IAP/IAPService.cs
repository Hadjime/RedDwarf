using System;
using System.Collections.Generic;
using System.Linq;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Data.PlayerResources;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEditor;
using UnityEngine.Purchasing;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	public class IAPService : IIAPService
	{
		private IAPProvider _iapProvider;
		private readonly IPersistentProgressService _progressService;


		public bool IsInitialized => _iapProvider.IsInitialized;
		public event Action Initialized;


		public IAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
		{
			_iapProvider = iapProvider;
			_progressService = progressService;
		}
		
		public void Initialize()
		{
			_iapProvider.Initialize(this);
			_iapProvider.Initialized += () => Initialized?.Invoke();  
		}


		public List<ProductDescription> Products() =>
			ProductsDescriptions().ToList();


		public void StartPurchase(string productId) =>
			_iapProvider.StartPurchase(productId);


		public PurchaseProcessingResult ProcessPurchase(Product purchaseProduct)
		{
			ProductConfig productConfig = _iapProvider.Configs[purchaseProduct.definition.id];

			switch (productConfig.ResourceType)
			{
				case ResourceType.Gold:
					_progressService.Progress.ResourceData.Add(productConfig.ResourceType, productConfig.Quantity);
					_progressService.Progress.PurchasesData.AddPurchase(purchaseProduct.definition.id);
					break;

				case ResourceType.Pick:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			return PurchaseProcessingResult.Complete;
		}


		private IEnumerable<ProductDescription> ProductsDescriptions()
		{
			PurchaseData purchasesData = _progressService.Progress.PurchasesData;
			foreach (string productId in _iapProvider.Products.Keys)
			{
				ProductConfig productConfig = _iapProvider.Configs[productId];
				Product product = _iapProvider.Products[productId];

				BoughtIAP boughtIap = purchasesData.BoughtIAPs.Find(iap => iap.IAPid == productId);
				
				if (ProductBoughtOut(boughtIap, productConfig))
					continue;

				yield return new ProductDescription
				{
					Id = productId,
					Config = productConfig,
					Product = product,
					AvailablePurchasesLeft = boughtIap != null 
						? productConfig.MaxPurchaseCount - boughtIap.Count 
						: productConfig.MaxPurchaseCount
				};
			}
		}


		private bool ProductBoughtOut(BoughtIAP boughtIap, ProductConfig productConfig) =>
			boughtIap != null && boughtIap.Count >= productConfig.MaxPurchaseCount;
	}
}
