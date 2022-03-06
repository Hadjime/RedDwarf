using InternalAssets.Scripts.Infrastructure.Services;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		void CreateShop();
		void CreateUIRoot();
		GameObject CreateHud();
	}
}
