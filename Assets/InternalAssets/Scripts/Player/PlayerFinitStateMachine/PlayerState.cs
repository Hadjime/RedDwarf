using InternalAssets.Scripts.Player.Data;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    public abstract class PlayerState
    {
        protected Player player;
        protected PlayerFSM playerFsm;
        protected PlayerData playerData;
        
        protected PlayerState(Player player, PlayerFSM playerFsm, PlayerData playerData, string animBoolName)
        {
            this.player = player;
            this.playerFsm = playerFsm;
            this.playerData = playerData;
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