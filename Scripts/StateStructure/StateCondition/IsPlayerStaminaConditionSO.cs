using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Actors;
using Config;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsPlayerStaminaConditionSO", menuName = "State Machines/Conditions/IsPlayerStaminaCondition")]
    public class IsPlayerStaminaConditionSO : StateConditionSO<IsPlayerStaminaCondition>
    {
        //���� ���¹̳� �̻��Ͻ� True
        public float stamina => _stamina;
        //���� ���¹̳� �����Ͻ� True
        public bool reverseMode => _isReverse;

        [SerializeField] float _stamina;
        [SerializeField] bool _isReverse;

        protected override Condition CreateCondition() => new IsPlayerStaminaCondition();
    }

    public class IsPlayerStaminaCondition : Condition
    {
        private IsPlayerStaminaConditionSO originSO => (IsPlayerStaminaConditionSO)base.OriginSO;

        Stats stats;
        public override void Awake(StateMachine _stateMachine)
        {
            stats = _stateMachine.GetComponent<Stats>();
        }


        protected override bool Statement()
        {
            if (originSO.reverseMode)
                if (stats.GetStamina() <= originSO.stamina)
                    return true;
                else
                    return false;
            return stats.GetStamina() >= originSO.stamina;
        }

    }
}

