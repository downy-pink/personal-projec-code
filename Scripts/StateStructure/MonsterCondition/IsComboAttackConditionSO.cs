using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsComboAttackConditionSO", menuName = "State Machines/Conditions/IsComboAttackCondition")]
    public class IsComboAttackConditionSO : StateConditionSO<IsStartCondition>
    {
        public StateSO comboAttackSO => _comboAttackSO;

        [SerializeField] StateSO _comboAttackSO;
        protected override Condition CreateCondition() => new IsComboAttackCondition();
    }

    public class IsComboAttackCondition : Condition
    {
        private IsComboAttackConditionSO originSO => (IsComboAttackConditionSO)base.OriginSO;

        StateMachine sm;
        public override void Awake(StateMachine _stateMachine)
        {
            sm = _stateMachine;
        }


        protected override bool Statement()
        {
            bool isComboAttack = false;
            if (sm.GetCurrentState() != null && sm.GetCurrentState().GetStateSO() == originSO.comboAttackSO)
                isComboAttack = true;
            else
                isComboAttack = false;

            return !isComboAttack;
        }
    }
}

