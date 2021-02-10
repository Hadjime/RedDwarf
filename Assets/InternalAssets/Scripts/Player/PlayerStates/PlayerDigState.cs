using InternalAssets.Scripts.Player.Data;
using InternalAssets.Scripts.Player.PlayerFinitStateMachine;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerStates
{
    public class PlayerDigState: PlayerState
    {
        private int isDiging;
        public PlayerDigState(PlayerFinitStateMachine.Player player, PlayerFSM playerFsm, PlayerData playerData, string animBoolName) : base(player, playerFsm, playerData, animBoolName)
        {
            isDiging = Animator.StringToHash(animBoolName);
        }

        public override void Enter()
        {
            base.Enter();
            player.animator.SetBool(isDiging, true);
        }

        public override void Exit()
        {
            base.Exit();
            player.animator.SetBool(isDiging, false);
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