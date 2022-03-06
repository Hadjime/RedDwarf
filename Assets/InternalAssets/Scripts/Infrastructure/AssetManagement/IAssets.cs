using System.Collections.Generic;
using InternalAssets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
		void LoadAllAsyncByLabel<T>(string path, System.Action<List<T>> onFinish);


		T LoadAsync<T>(string path);


		GameObject Instantiate(string path, Vector3 at, Transform parent);
	}
}