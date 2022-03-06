using InternalAssets.Scripts.UI.Services.Factory;
using SRDebugger;


namespace InternalAssets.Scripts.UI.Services.Windows
{
	public class WindowService : IWindowService
	{
		private IUIFactory _uiFactory;


		public WindowService() {}

		
		public void Initialize(IUIFactory uiFactory) =>
			_uiFactory = uiFactory;


		public void Open(WindowId windowId)
		{
			switch (windowId)
			{
				case WindowId.Unknown:
					break;

				case WindowId.Shop:
					_uiFactory.CreateShop();
					break;
			}
		}
	}
}
