
namespace Infrastructure.States
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void EnterScene(string sceneName);
        void EnterScene(int indexScene);
    }
}
