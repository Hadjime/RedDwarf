using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;


namespace InternalAssets.Scripts.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		void CreateShop();
		void CreateUIRoot();
	}
}
