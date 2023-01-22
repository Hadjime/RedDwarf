using UnityEngine;
using Zenject;

namespace InternalAssets.Scripts.Infrastructure.GameStateMachine
{
    [CreateAssetMenu(fileName = "GameStateMachineInstaller", menuName = "Installers/GameStateMachineInstaller")]
    public class GameStateMachineInstaller : ScriptableObjectInstaller<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }
    }
}