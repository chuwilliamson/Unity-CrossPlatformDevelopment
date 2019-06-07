using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat
{


    public class CombatBehaviour : MonoBehaviour
    {

    }
    public abstract class Entity
    {
        public string Name { get; set; }
        public Dictionary<string, CombatAction> Actions;
        public virtual void AddAction(string name, UnityEngine.Events.UnityAction action)
        {
            Actions.Add(name, new CombatAction { Name = name, Response = action });
        }
    }

    public class CombatFSM
    {

    }


    public interface IContext
    {
        void ResetContext();
        IState CurrentState { get; }
        void UpdateContext();
        void ChangeState(IState next);
    }

    public interface IState
    {
        void OnEnter(IContext context);
        void UpdateState(IContext context);
        void OnExit(IContext context);
    }
    public abstract class State : IState
    {
        public abstract void OnEnter(IContext context);
        public abstract void OnExit(IContext context);
        public abstract void UpdateState(IContext context);
    }
    public abstract class CombatState : State
    {
    }

    public class CombatStart : CombatState
    {
        public override void OnEnter(IContext context)
        {

        }

        public override void OnExit(IContext context)
        {

        }

        public override void UpdateState(IContext context)
        {

        }
    }

    public class CombatResolve : CombatState
    {
        public override void OnEnter(IContext context)
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit(IContext context)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState(IContext context)
        {
            throw new System.NotImplementedException();
        }
    }

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

    public class CombatContext : Context
    {
        public override void ResetContext()
        {
            CurrentState = new CombatStart();
        }
    }
}