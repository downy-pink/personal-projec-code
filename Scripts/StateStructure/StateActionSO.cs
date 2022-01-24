using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    
    public abstract class StateActionSO  : ScriptableObject
    {

        //StateSO�� ���ؼ� ȣ��ȴ�.
        //StateAction�� �����ϱ����ؼ� �ʿ���
        internal StateAction GetAction(StateMachine stateMachine)
        {
            var action = CreateAction();
            action.originSO = this;
            action.Awake(stateMachine);
            return action;
        }

        protected abstract StateAction CreateAction();
    }

    //StateAction�� ������Ų��.
    public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
    {
        protected override StateAction CreateAction() => new T();
    }
}

