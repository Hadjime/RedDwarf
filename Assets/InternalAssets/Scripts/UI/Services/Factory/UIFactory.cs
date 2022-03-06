using InternalAssets.Scripts.Infrastructure.Ads;
using InternalAssets.Scripts.Infrastructure.AssetManagement;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using InternalAssets.Scripts.Infrastructure.Services.StaticData;
using InternalAssets.Scripts.StaticData.Windows;
using InternalAssets.Scripts.UI.Elements;
using InternalAssets.Scripts.UI.GamePlay;
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
		private readonly IWindowService _windowService;
		private Transform _uiRoot;


		public UIFactory(
				IAssets assets,
				IStaticDataService staticData,
				IPersistentProgressService progressService,
				IAdsService adsService,
				IWindowService windowService
			) {
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_adsService = adsService;
			_windowService = windowService;
		}


		public void CreateUIRoot() =>
			_uiRoot = _assets.Instantiate(UI_ROOT_PATH).transform;


		public void CreateShop()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.Shop);
			WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
			window.Constructor(_progressService);
		}


		public GameObject CreateHud()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.GamePlay);
			WindowBase hud = Object.Instantiate(config.Prefab, _uiRoot);

			hud.GetComponentInChildren<LootCounter>()
			   ?.Constructor(_progressService.Progress.WorldData);

			foreach (OpenWindowButton openWindowButton in hud
				.GetComponentsInChildren<OpenWindowButton>())
			{
				openWindowButton.Constructor(_windowService);
			}
			
			return hud.gameObject;
		}
	}
}
