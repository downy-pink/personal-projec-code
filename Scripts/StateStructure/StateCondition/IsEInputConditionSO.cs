using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsEInputConditionSO", menuName = "State Machines/Conditions/IsEInputCondition")]
    public class IsEInputConditionSO : StateConditionSO<IsEInputCondition>
    {
        protected override Condition CreateCondition() => new IsEInputCondition();
    }

    public class IsEInputCondition : Condition
    {
        private IsEInputConditionSO originSO => (IsEInputConditionSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }


        protected override bool Statement()
        {
            return player.GetIsEInput();
        }
    }
}

