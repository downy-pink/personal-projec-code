using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsRInputConditionSO", menuName = "State Machines/Conditions/IsRInputCondition")]
    public class IsRInputConditionSO : StateConditionSO<IsRInputCondition>
    {
        protected override Condition CreateCondition() => new IsRInputCondition();
    }
    public class IsRInputCondition : Condition
    {
        private IsRInputConditionSO originSO => (IsRInputConditionSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }


        protected override bool Statement()
        {
            return player.GetIsRInput();
        }
    }
}
