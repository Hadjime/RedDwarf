using InternalAssets.Scripts.Infrastructure.Ads;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.StaticData.Windows;
using InternalAssets.Scripts.UI.Services.Windows;
using InternalAssets.Scripts.UI.Windows;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Services.Factory
{
	class UIFactory : IUIFactory
	{
		private const string UI_ROOT_PATH = "UIRoot";
		
		private readonly IAssets _assets;
		private readonly IStaticDataService _staticData;
		private readonly IPersistentProgressService _progressService;
		private readonly IAdsService _adsService;
		private Transform _uiRoot;


		public UIFactory(
				IAssets assets,
				IStaticDataService staticData,
				IPersistentProgressService progressService,
				IAdsService adsService
			) {
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_adsService = adsService;
		}


		public void CreateUIRoot() =>
			_uiRoot = _assets.Instantiate(UI_ROOT_PATH).transform;


		public void CreateShop()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.Shop);
			WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
			window.Constructor(_progressService);
		}
	}
}
