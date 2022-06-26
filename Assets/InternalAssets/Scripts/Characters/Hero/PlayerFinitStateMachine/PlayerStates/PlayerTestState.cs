using InternalAssets.Scripts.Characters.Hero.Data;


namespace InternalAssets.Scripts.Characters.Hero.PlayerFinitStateMachine.PlayerStates
{
    public class PlayerTestState : PlayerState
    {
        public PlayerTestState(PlayerFinitStateMachine.Player player, PlayerFSM playerFsm, PlayerData playerData,
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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}