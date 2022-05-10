using InternalAssets.Scripts.Player.Data;
using InternalAssets.Scripts.Player.PlayerFinitStateMachine;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerStates
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(PlayerFinitStateMachine.Player player, PlayerFSM playerFsm, PlayerData playerData,
            int animBoolId) : base(player, playerFsm, playerData, animBoolId)
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
            
            
            
            if (player.inputHandler.RawMovementInput == Vector2.zero)
            {
                player.playerFSM.ChangeState(player.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            player.Movement();
        }
    }
}