using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsHPCheckConditionSO", menuName = "State Machines/Conditions/IsHPCheckCondition")]
    public class IsHPCheckConditionSO : StateConditionSO<IsHitCondition>
    {
        public float hpCheck => _hpCheck;

        [SerializeField] float _hpCheck; //해당 hp 이하면 true
        protected override Condition CreateCondition() => new IsHPCheckCondition();
    }

    public class IsHPCheckCondition : Condition
    {
        private IsHPCheckConditionSO originSO => (IsHPCheckConditionSO)base.OriginSO;
        Stats stats;

        public override void Awake(StateMachine _stateMachine)
        {
            stats = _stateMachine.GetComponent<Stats>();
        }

        protected override bool Statement() => stats.GetHP() <= originSO.hpCheck;
    }
}

