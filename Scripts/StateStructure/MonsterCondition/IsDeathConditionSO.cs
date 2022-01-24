using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsDeathConditionSO", menuName = "State Machines/Conditions/IsDeathCondition")]
    public class IsDeathConditionSO : StateConditionSO<IsBlockCondition>
    {
        protected override Condition CreateCondition() => new IsDeathCondition();
    }
    public class IsDeathCondition : Condition
    {
        private IsDeathConditionSO originSO => (IsDeathConditionSO)base.OriginSO;

        Stats stats;

        public override void Awake(StateMachine _stateMachine)
        {
            stats = _stateMachine.GetComponent<Stats>();
        }


        protected override bool Statement()
        {
            return stats.GetHP() <= 0;
        }
    }
}

