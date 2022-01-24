using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsHitConditionSO", menuName = "State Machines/Conditions/IsHitCondition")]
    //맞았다는 정보를 받음
    public class IsHitConditionSO : StateConditionSO<IsHitCondition>
    {
        protected override Condition CreateCondition() => new IsHitCondition();
    }

    public class IsHitCondition : Condition
    {
        private IsHitConditionSO originSO => (IsHitConditionSO)base.OriginSO;

        Actor actor;
        bool ishit;

        public override void Awake(StateMachine _stateMachine)
        {
            actor = _stateMachine.GetComponent<Actor>();
            actor.hitEvent += hitTrue;
        }

        public void hitTrue()
        {
            ishit = true;
        }

        protected override bool Statement()
        {
            if(ishit)
            {
                ishit = false;
                return true;
            }
            return ishit;
        }
    }
}

