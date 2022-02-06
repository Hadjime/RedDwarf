using UnityEngine;

namespace InternalAssets.Scripts.Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
    }
}