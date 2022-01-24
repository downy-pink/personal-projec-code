using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    public abstract class StateConditionSO : ScriptableObject
    {
        internal StateCondition GetCondition(StateMachine _stateMachine, Dictionary<ScriptableObject, object> _createdInstances)
        {
            if (!_createdInstances.TryGetValue(this, out var obj))
            {
                var condition = CreateCondition();
                condition.originSO = this;
                _createdInstances.Add(this, condition);
                condition.Awake(_stateMachine);

                obj = condition;
            }

            return new StateCondition(_stateMachine, (Condition)obj);
        }
        protected abstract Condition CreateCondition();
    }

    public abstract class StateConditionSO<T> : StateConditionSO where T : Condition, new()
    {
        protected override Condition CreateCondition() => new T();
    }
}

