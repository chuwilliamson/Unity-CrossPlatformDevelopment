namespace Combat
{
    public abstract class State : IState
    {
        public abstract void OnEnter(IContext context);
        public abstract void OnExit(IContext context);
        public abstract void UpdateState(IContext context);
    }
}