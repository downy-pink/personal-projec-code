using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    //����Ȯ��, ����Ͽ� Ȯ�强�� ������.
    public abstract class Condition : IStateFlow
    {
        
        bool isCached = false;
        bool cachedStatement;
        internal StateConditionSO originSO;

        protected StateConditionSO OriginSO => originSO;

        protected abstract bool Statement();

        internal bool GetStatement()
        {
            //if (!isCached) //�ʿ伺������ ���
            //{
            //    isCached = true;
            //    cachedStatement = Statement();
            //}
            cachedStatement = Statement();
            return cachedStatement;
        }

        internal void ClearStatementCache()
        {
            isCached = false;
        }

        public virtual void Awake(StateMachine _stateMachine) { }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }

    public readonly struct StateCondition
    {
        internal readonly StateMachine stateMachine;
        internal readonly Condition condition;

        public StateCondition(StateMachine _stateMachine, Condition _conditiont)
        {
            stateMachine = _stateMachine;
            condition = _conditiont;
        }

        public bool IsMet()
        {
            bool statement = condition.GetStatement();
            bool isMet = statement;

            return isMet;
        }

    }
}

