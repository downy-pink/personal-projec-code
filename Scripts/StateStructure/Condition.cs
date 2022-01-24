using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    //조건확인, 상속하여 확장성을 가진다.
    public abstract class Condition : IStateFlow
    {
        
        bool isCached = false;
        bool cachedStatement;
        internal StateConditionSO originSO;

        protected StateConditionSO OriginSO => originSO;

        protected abstract bool Statement();

        internal bool GetStatement()
        {
            //if (!isCached) //필요성느낄때 사용
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

