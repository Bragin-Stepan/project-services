using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.InputManagement
{
    public interface IPlayerInputService : IInput
    {
        event Action OnJump;
        
        event Action OnInteract;
        
        event Action OnPrevious;
        
        event Action OnNext;
        
        Vector2 Move { get; }
        
        string GetKeyboardInput();
    }
}