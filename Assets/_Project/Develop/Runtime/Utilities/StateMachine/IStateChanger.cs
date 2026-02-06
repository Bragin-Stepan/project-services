using System;

namespace _Project.Develop.Runtime.Utilities.StateMachine
{
    public interface IStateChanger
    {
        event Action<State> Changed;
        
        StateMachine ChangeState<TState>() where TState : State;
    }
}