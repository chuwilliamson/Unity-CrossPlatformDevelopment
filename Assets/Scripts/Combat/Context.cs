using UnityEngine;

namespace Combat
{
    public abstract class Context : IContext
    {
        //we store references here to handle the disabling of monobehaviours specific to 
        //the states. EX: Interacting state will disable the playercontroller
        public StateMachineBehaviour Behaviour { get; set; }
        private IState _currentState;
        public IState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                _currentState.OnEnter(this);
            }
        }

        public void UpdateContext()
        {
            _currentState.UpdateState(this);
        }

        public abstract void ResetContext();



        public virtual void ChangeState(IState next)
        {
            _currentState.OnExit(this);
            _currentState = next;
            _currentState.OnEnter(this);
        }
    }
}