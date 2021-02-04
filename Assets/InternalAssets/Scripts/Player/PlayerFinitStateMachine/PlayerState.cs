using InternalAssets.Scripts.Player.Data;
using UnityEngine;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    public abstract class PlayerState
    {
        protected Player player;
        protected PlayerFSM playerFsm;
        protected PlayerData playerData;
        
        private string _animBoolName;
        
        protected PlayerState(Player player, PlayerFSM playerFsm, PlayerData playerData, string animBoolName)
        {
            this.player = player;
            this.playerFsm = playerFsm;
            this.playerData = playerData;
            this._animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            Debug.Log("Enter state " + player._playerFSM.CurrentState.ToString());
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