using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StateStructure
{
    //State를 초기화하고 State의 리스트를 가지는곳
    [CreateAssetMenu(fileName = "TransitionSO", menuName = "State Machines/TransitionSO")]
    public class TransitionSO : ScriptableObject
    {
        [SerializeField] TransitionItem[] transitions = default;


        internal List<State> GetInitialState(StateMachine _stateMachine)
        {
          
            List<State> _stateList = new List<State>();
            var _createdInstances = new Dictionary<ScriptableObject, object>();

            for (int i = 0; i < transitions.Length; ++i)
            {
                State _state = transitions[i].stateSO.GetState(_stateMachine);
                _state.originSO = transitions[i].stateSO;
                StateTransition _startTransition = new StateTransition(TransitionInit(_stateMachine, transitions[i].startStateConditionSO, _createdInstances));
                StateTransition _endTransition = new StateTransition(TransitionInit(_stateMachine, transitions[i].endStateConditionSO, _createdInstances));
                _state.startTransitions = _startTransition;
                _state.endTransitions = _endTransition;
                _state.isEndOrMode = transitions[i].isEndOrMode;
                _stateList.Add(_state);
            }
            return _stateList;
        }

         StateCondition[] TransitionInit(StateMachine _stateMachine, StateConditionSO[] _conditionSO, Dictionary<ScriptableObject, object> _createdInstances)
        {
            List<StateConditionSO> _stateConditionSOList = new List<StateConditionSO>();
            List<StateCondition> _stateConditionList = new List<StateCondition>();
            for (int i = 0; i < _conditionSO.Length; ++i)
            {
                _stateConditionSOList.Add(_conditionSO[i]);
                _stateConditionList.Add(_stateConditionSOList[i].GetCondition(_stateMachine, _createdInstances));
            }
            return _stateConditionList.ToArray();
        }

        [Serializable]
        public struct TransitionItem
        {
            public StateSO stateSO;
            public StateConditionSO[] startStateConditionSO;
            public StateConditionSO[] endStateConditionSO;
            public bool isEndOrMode;
        }

    }

}
