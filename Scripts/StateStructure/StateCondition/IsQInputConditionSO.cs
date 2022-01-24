using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsQInputConditionSO", menuName = "State Machines/Conditions/IsQInputCondition")]
    public class IsQInputConditionSO : StateConditionSO<IsStartCondition>
    {
        protected override Condition CreateCondition() => new IsQInputCondition();
    }

    public class IsQInputCondition : Condition
    {
        private IsQInputConditionSO originSO => (IsQInputConditionSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }


        protected override bool Statement()
        {
            return player.GetIsQInput();
        }
    }
}

