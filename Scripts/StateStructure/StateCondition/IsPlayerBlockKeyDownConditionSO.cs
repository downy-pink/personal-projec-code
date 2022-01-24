using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsPlayerBlockKeyDownConditionSO", menuName = "State Machines/Conditions/IsPlayerBlockKeyDownCondition")]
    public class IsPlayerBlockKeyDownConditionSO : StateConditionSO<IsPlayerBlockKeyDownCondition>
    {
        public bool reverseMode => _reverseMode;

        [SerializeField] bool _reverseMode;
        protected override Condition CreateCondition() => new IsPlayerBlockKeyDownCondition();
    }

    public class IsPlayerBlockKeyDownCondition : Condition
    {
        private IsPlayerBlockKeyDownConditionSO originSO => (IsPlayerBlockKeyDownConditionSO)base.OriginSO;

        PlayerActor player;

        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();

        }

        protected override bool Statement()
        {
            if (originSO.reverseMode)
                return !player.isRightMouseClick;
            return player.isRightMouseClick;
        }
    }
}
