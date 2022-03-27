using System;
using InternalAssets.Scripts.Data.PlayerResources;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEditor;
using UnityEngine.Purchasing;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	public class IAPService : IService
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
			_iapProvider.Initialize();
			_iapProvider.Initialized += () => Initialized?.Invoke();  
		}


		public void StartPurchase(string productId) =>
			_iapProvider.StartPurchase(productId);


		public PurchaseProcessingResult ProcessPurchase(Product purchaseProduct)
		{
			ProductConfig productConfig = _iapProvider.Configs[purchaseProduct.definition.id];

			switch (productConfig.ResourceType)
			{
				case ResourceType.Gold:
					_progressService.Progress.ResourceData.Add(productConfig.ResourceType, productConfig.Quantity);
					break;

				case ResourceType.Pick:
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
