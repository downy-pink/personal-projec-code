using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(menuName = "State Machines/Conditions/Is Moving")]
    public class IsMovingConditionSO : StateConditionSO<IsMovingCondition>
    {
        [Tooltip("멈춰있는것을 확인하는 모드 On")]
        public bool isStopping;

        protected override Condition CreateCondition() => new IsMovingCondition();
    }

    public class IsMovingCondition : Condition
    {
        PlayerActor player;
        private IsMovingConditionSO originSO => (IsMovingConditionSO)base.OriginSO;

        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }

        protected override bool Statement()
        {    
            Vector3 _moveInputVector = player.GetmoveInputValue();
            bool _isMoving = _moveInputVector.sqrMagnitude > 0;
            if (originSO.isStopping)
                _isMoving = !_isMoving;
            return _isMoving;
        }
    }
}

