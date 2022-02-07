using InternalAssets.Scripts.Services;
using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, GameObject at);
    }
}