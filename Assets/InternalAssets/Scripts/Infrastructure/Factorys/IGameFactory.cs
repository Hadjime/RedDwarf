using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
    }
}