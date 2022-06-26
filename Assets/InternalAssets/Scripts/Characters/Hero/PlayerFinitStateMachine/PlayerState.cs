using InternalAssets.Scripts.Characters.Hero.Data;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero.PlayerFinitStateMachine
{
    public abstract class PlayerState
    {
        protected Player player;
        protected PlayerFSM playerFsm;
        protected PlayerData playerData;
        protected int animBoolId;
        
        private string _animBoolName;
        
        protected PlayerState(Player player, PlayerFSM playerFsm, PlayerData playerData, int animBoolId)
        {
            this.player = player;
            this.playerFsm = playerFsm;
            this.playerData = playerData;
            this.animBoolId = animBoolId;
        }

        public virtual void Enter()
        {
            Debug.Log("Enter state " + player.playerFSM.CurrentState.ToString());
            player.animator.SetBool(animBoolId, true);
        }

        public virtual void Exit()
        {
            player.animator.SetBool(animBoolId, false);
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }
    }
}