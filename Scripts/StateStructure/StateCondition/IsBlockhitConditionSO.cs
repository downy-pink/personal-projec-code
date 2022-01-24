using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsBlockhitConditionSO", menuName = "State Machines/Conditions/IsBlockhitCondition")]
    public class IsBlockhitConditionSO : StateConditionSO<IsStartCondition>
    {
        protected override Condition CreateCondition() => new IsBlockhitCondition();
    }

    public class IsBlockhitCondition : Condition
    {
        private IsBlockhitConditionSO originSO => (IsBlockhitConditionSO)base.OriginSO;

        Actor actor;
        public override void Awake(StateMachine _stateMachine)
        {
            actor = _stateMachine.GetComponent<Actor>();
        }


        protected override bool Statement()
        {
            return actor.GetIsBlockhit();
        }
    }
}

