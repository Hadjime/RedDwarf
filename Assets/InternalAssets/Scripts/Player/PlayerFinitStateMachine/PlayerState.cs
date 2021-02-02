using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    public abstract class PlayerState
    {
        protected PlayerFSM PlayerFsm;
        
        protected PlayerState(PlayerFSM playerFsm)
        {
            this.PlayerFsm = playerFsm;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }
    }
}