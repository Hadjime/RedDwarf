using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, GameObject at);
    }
}