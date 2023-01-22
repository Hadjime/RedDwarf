using UnityEngine;
using Zenject;

namespace InternalAssets.Scripts.Infrastructure.Gameplay
{
    [CreateAssetMenu(fileName = "WorldGameInstaller", menuName = "Installers/WorldGameInstaller")]
    public class WorldGameInstaller : ScriptableObjectInstaller<WorldGameInstaller>
    {
        public override void InstallBindings()
        {
            // Container.BindInterfacesTo<WorldGame>();
            Container.BindInterfacesAndSelfTo<WorldGame>().AsSingle();
        }
    }
}