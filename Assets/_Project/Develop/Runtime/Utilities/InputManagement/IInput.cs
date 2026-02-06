namespace _Project.Develop.Runtime.Utilities.InputManagement
{
    public interface IInput
    {
        bool IsEnabled { get; set; }
        
        void Enable();
        
        void Disable();
        
        void Update(float deltaTime);
    }
}