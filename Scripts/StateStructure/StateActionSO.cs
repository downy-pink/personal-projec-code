using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    
    public abstract class StateActionSO  : ScriptableObject
    {

        //StateSO를 통해서 호출된다.
        //StateAction을 생성하기위해서 필요함
        internal StateAction GetAction(StateMachine stateMachine)
        {
            var action = CreateAction();
            action.originSO = this;
            action.Awake(stateMachine);
            return action;
        }

        protected abstract StateAction CreateAction();
    }

    //StateAction을 생성시킨다.
    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction CreateAction() => new T();
    }
}

