namespace CustomExtensions.State_Machine
{
    public interface IState
    {
        void OnEnter();
        void Update();
    }
}