using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Actors;
using Config;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsStaminaCheckConditionSO", menuName = "State Machines/Conditions/IsStaminaCheckCondition")]
    public class IsStaminaCheckConditionSO : StateConditionSO<IsStaminaCheckCondition>
    {
        //���� ���¹̳� �̻��Ͻ� True
        public float stamina => _stamina;
        //���� ���¹̳� �����Ͻ� True
        public bool reverseMode => _isReverse;
        public float evasionConsumeStamina => evasionStaminaConsumeConfigSO.evasionConsume;

        [SerializeField] float _stamina;
        [SerializeField] bool _isReverse;
        [SerializeField] EvasionStaminaConsumeConfigSO evasionStaminaConsumeConfigSO;
        protected override Condition CreateCondition() => new IsStaminaCheckCondition();
    }

    public class IsStaminaCheckCondition : Condition
    {
        private IsStaminaCheckConditionSO originSO => (IsStaminaCheckConditionSO)base.OriginSO;

        Stats stats;
        public override void Awake(StateMachine _stateMachine)
        {
            stats = _stateMachine.GetComponent<Stats>();
        }



        protected override bool Statement()
        {
            if (originSO.reverseMode)
                if (stats.GetStamina() < originSO.stamina && stats.GetStamina() - originSO.evasionConsumeStamina >= 0)
                    return true;
                else
                    return false;
            return stats.GetStamina() >= originSO.stamina;
        }

    }
}

