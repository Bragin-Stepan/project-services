using System;

namespace _Project.Develop.Runtime.Utilities.StateMachine
{
    public abstract class State
    {
        public event Action Entered;
        
        public event Action Exited;

        public virtual void OnEnter() => Entered?.Invoke();

        public virtual void OnExit() => Exited?.Invoke();
        
        public virtual void Update(float deltaTime) { }
    }
}