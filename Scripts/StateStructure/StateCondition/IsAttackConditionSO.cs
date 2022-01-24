using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsAttackConditionSO", menuName = "State Machines/Conditions/IsAttack Condition")]
    public class IsAttackConditionSO : StateConditionSO<IsAttackCondition>
    {
        protected override Condition CreateCondition() => new IsAttackCondition();
    }


    public class IsAttackCondition : Condition
    {
        private IsAttackConditionSO originSO => (IsAttackConditionSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }

        protected override bool Statement()
        {
            return player.isAttack;
        }
    }
}
