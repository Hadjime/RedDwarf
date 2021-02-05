using InternalAssets.Scripts.Player.Data;
using UnityEngine;
using InternalAssets.Scripts.Player.PlayerFinitStateMachine;

namespace InternalAssets.Scripts.Player.PlayerStates
{
    public class PlayerTestState : PlayerState
    {
        public PlayerTestState(PlayerFinitStateMachine.Player player, PlayerFSM playerFsm, PlayerData playerData, string animBoolName) : base(player, playerFsm, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}