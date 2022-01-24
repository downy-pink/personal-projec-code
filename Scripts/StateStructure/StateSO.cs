using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "StateSO", menuName = "State Machines/StateSO")]
    public class StateSO : ScriptableObject
    {
        //[SerializeField] string stateName;

        [SerializeField] StateActionSO[] stateAcitionSO;
        [SerializeField] StateSO[] blockStatesSO; //���� �� Ȯ���Ͽ� �������� �����ϴ� ����
        [SerializeField] StateSO[] stopStatesSO; //���� �� ����� �ϴ� ����

        //[SerializeField] string[] stopStateName;
        //[SerializeField] string[] blockStateName;
        internal State GetState(StateMachine stateMachine)
        {
            var state = new State();
            state.originSO = this;
            state.stateMachine = stateMachine;
            state.actions = GetActions(stateAcitionSO, stateMachine);
            state.startTransitions = new StateTransition();
            state.endTransitions = new StateTransition();
            state.blockStatesSO = blockStatesSO;
            state.stopStatesSO = stopStatesSO;
            //state.stateName = stateName;
            //state.stopStateName = stopStateName;
            //state.blockStateName = blockStateName;
            return state;
        }
        
        
        private StateAction[] GetActions(StateActionSO[] scriptableActions,
            StateMachine stateMachine)
        {
            int count = scriptableActions.Length;
            var _actions = new StateAction[count];
            for (int i = 0; i < count; ++i)
                _actions[i] = scriptableActions[i].GetAction(stateMachine);

            return _actions;
        }

    }

}
