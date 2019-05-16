namespace GameEventSystem
{
    public interface IListener
    {
        void Subscribe();
        void UnSubscribe();
        void OnEventRaised(params object[] args);
    }
}