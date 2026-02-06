using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Utilities.InputManagement
{
    public class DesktopPlayerInputService : IPlayerInputService
    {
        public event Action OnJump;
        public event Action OnInteract;
        public event Action OnPrevious;
        public event Action OnNext;
        
        private const string HorizontalAxisKey = "Horizontal";
        private const string VerticalAxisKey = "Vertical";
        
        private const KeyCode JumpKey = KeyCode.Space;
        private const KeyCode InteractKey = KeyCode.F;
        private const KeyCode PreviousKey = KeyCode.Q;
        private const KeyCode NextKey = KeyCode.E;

        public bool IsEnabled { get; set; } = true;

        public Vector2 Move
        {
            get
            {
                if (IsEnabled == false)
                    return Vector2.zero;

                return new Vector2(Input.GetAxisRaw(HorizontalAxisKey), Input.GetAxisRaw(VerticalAxisKey));
            }
        }
        
        public void Enable() => IsEnabled = true;
        
        public void Disable() => IsEnabled = false;
        
        public void Update(float deltaTime)
        {
            if (IsEnabled == false)
                return;
            
            if (Input.GetKeyDown(JumpKey))
                OnJump?.Invoke();
            
            if (Input.GetKeyDown(InteractKey))
                OnInteract?.Invoke();
            
            if (Input.GetKeyDown(PreviousKey))
                OnPrevious?.Invoke();
            
            if (Input.GetKeyDown(NextKey))
                OnNext?.Invoke();
        }

        public string GetKeyboardInput()
        {
            if (IsEnabled == false)
                return null;
            
            for (int i = 0; i <= 9; i++)
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                    return i.ToString();
            
            for (int i = 0; i < 26; i++)
            {
                if (Input.GetKeyDown(KeyCode.A + i))
                {
                    char letter = (char)('A' + i);
                    return letter.ToString();
                }
            }
            
            return null;
        }
    }
}