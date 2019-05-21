namespace Chuwilliamson.GameEventSystem
{
    public interface ISubscribeable
    {
        void RegisterListener(IListener listener);
        void UnRegisterListener(IListener listener);

        void Raise(params object[] args);
    }
}