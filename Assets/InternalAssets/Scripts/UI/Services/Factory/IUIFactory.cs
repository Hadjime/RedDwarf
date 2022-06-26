using System.Threading.Tasks;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Services.Factory
{
	public interface IUIFactory : IService
	{
		Task CreateShop();
		Task CreateUIRoot();
		Task<GameObject> CreateHud();
	}
}
