namespace InternalAssets.Scripts.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}