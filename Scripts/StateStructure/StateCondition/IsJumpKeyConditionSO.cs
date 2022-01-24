using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsJumpKeyCondition", menuName = "State Machines/Conditions/IsJumpKeyPress")]
    public class IsJumpKeyConditionSO : StateConditionSO<IsJumpKeyCondition>
    {
        protected override Condition CreateCondition() => new IsJumpKeyCondition();
    }


    public class IsJumpKeyCondition : Condition
    {
        private IsJumpKeyConditionSO originSO => (IsJumpKeyConditionSO)base.OriginSO;

        PlayerActor player;

        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }

        protected override bool Statement()
        {
            return player.isjump;
        }
    }
}
