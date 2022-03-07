using System.Collections.Generic;
using System.Threading.Tasks;
using InternalAssets.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace InternalAssets.Scripts.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
		void Initialize();
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
		void LoadAllAsyncByLabel<T>(string path, System.Action<List<T>> onFinish);
		T LoadAsync<T>(string path);
		GameObject Instantiate(string path, Vector3 at, Transform parent);
		Task<T> Load<T>(AssetReference assetReference) where T : class;
		Task<T> Load<T>(string address) where T : class;
		void CleanUp();
	}
}