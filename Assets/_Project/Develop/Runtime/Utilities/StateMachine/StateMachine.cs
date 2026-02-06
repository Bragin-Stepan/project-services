using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Utilities.StateMachine
{
    public abstract class StateMachine : IStateChanger
    {
        public event Action<State> Changed;
        
        public State Current { get; private set; }

        private readonly Dictionary<Type, State> _states = new();
        private bool _isRunning;
        
        public StateMachine(params State[] states)
        {
            foreach (State state in states)
                Add(state);
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;
            
            Current?.Update(deltaTime);
        }
        
        public StateMachine Add(State state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            
            Type type = state.GetType();
            
            if (_states.TryAdd(type, state) == false)
                throw new InvalidOperationException($"[StateMachine] State {type.Name} is already registered");

            return this;
        }
        
        public StateMachine Remove<TState>() where TState : State
        {
            Type type = typeof(TState);
            
            if (_states.Remove(type) == false)
                throw new InvalidOperationException($"[StateMachine] State {type.Name} is not registered");

            return this;
        }
        
        public StateMachine ChangeState<TState>() where TState : State
        {
            Type type = typeof(TState);
            
            if (_states.TryGetValue(type, out State state) == false)
                throw new InvalidOperationException($"[StateMachine] State {type.Name} is not registered");
            
            SwitchState(state);
            
            return this;
        }
        
        private void SwitchState(State newState)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));
            
            if (Current != null)
                Current.OnExit();
            else
                _isRunning = true;
            
            Current = newState;
            Changed?.Invoke(Current);
            Current.OnEnter();
        }
    }
}