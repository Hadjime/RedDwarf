using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;


namespace InternalAssets.Scripts.UI.Services.Windows
{
	public interface IWindowService : IService
	{
		void Open(WindowId windowId);
	}
}
