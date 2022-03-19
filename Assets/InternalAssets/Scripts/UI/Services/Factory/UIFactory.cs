using System.Threading.Tasks;
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


		public async Task CreateUIRoot()
		{
			GameObject gameObject = await _assets.InstantiateAsync(UI_ROOT_PATH);
			_uiRoot = gameObject.transform;
		}


		public async Task CreateShop()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.Shop);
			GameObject shopPrefab = await _assets.LoadAsync<GameObject>(config.Prefab);
			GameObject shop = Object.Instantiate(shopPrefab, _uiRoot);
			shop.TryGetComponent(out WindowBase shopWindow);
			shopWindow.Constructor(_progressService);
		}


		public async Task<GameObject> CreateHud()
		{
			WindowConfig config = _staticData.ForWindow(WindowId.GamePlay);
			GameObject hudPrefab = await _assets.LoadAsync<GameObject>(config.Prefab);
			var hud = Object.Instantiate(hudPrefab, _uiRoot);


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
