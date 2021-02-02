using UnityEngine.InputSystem.LowLevel;

namespace InternalAssets.Scripts.Player.PlayerFinitStateMachine
{
    public class PlayerFSM
    {
        public PlayerState CurrentState { get; private set; }

        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}