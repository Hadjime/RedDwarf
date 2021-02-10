using InternalAssets.Scripts.Player.Data;
using InternalAssets.Scripts.Player.PlayerFinitStateMachine;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerStates
{
    public class PlayerMoveState : PlayerState
    {
        private int isMove;
        public PlayerMoveState(PlayerFinitStateMachine.Player player, PlayerFSM playerFsm, PlayerData playerData, string animBoolName) : base(player, playerFsm, playerData, animBoolName)
        {
            isMove = Animator.StringToHash(animBoolName);
        }

        public override void Enter()
        {
            base.Enter();
            player.animator.SetBool(isMove, true);
        }

        public override void Exit()
        {
            base.Exit();
            player.animator.SetBool(isMove, false);
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