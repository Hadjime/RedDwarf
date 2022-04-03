using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Infrastructure.Services;


namespace InternalAssets.Scripts.Infrastructure.IAP
{
	public interface IIAPService : IService
	{
		bool IsInitialized { get; }
		event Action Initialized;


		void Initialize();


		List<ProductDescription> Products();


		void StartPurchase(string productId);
	}
}
