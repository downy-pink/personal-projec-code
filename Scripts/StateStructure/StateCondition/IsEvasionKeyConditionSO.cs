using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsEvasionKeyConditionSO", menuName = "State Machines/Conditions/IsEvasionKeyCondition")]
    public class IsEvasionKeyConditionSO : StateConditionSO<IsEvasionKeyCondition>
    {
        public bool reverseMode => _reverseMode;

        [SerializeField] bool _reverseMode;
        protected override Condition CreateCondition() => new IsEvasionKeyCondition();
    }

    public class IsEvasionKeyCondition : Condition
    {
        private IsEvasionKeyConditionSO originSO => (IsEvasionKeyConditionSO)base.OriginSO;

        PlayerActor player;

        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();

        }

        protected override bool Statement()
        {
            if (originSO.reverseMode)
                return !player.isEvasionKey;
            return player.isEvasionKey;
        }
    }
}

