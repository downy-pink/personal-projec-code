using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;

namespace StateStructure
{
    //������ ���� ���� Ȯ��
    [CreateAssetMenu(fileName = "IsGroundCondition", menuName = "State Machines/Conditions/IsGroundCondition")]
    public class IsGroundConditionSO : StateConditionSO<IsGroundCondition>
    {
        [Tooltip("���߿� ���ִ��� üũ�ϴ� ��� On")]
        public bool isAirCheckMode;
        protected override Condition CreateCondition() => new IsGroundCondition();
    }

    public class IsGroundCondition : Condition
    {
        private IsGroundConditionSO originSO => (IsGroundConditionSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();

        }

        protected override bool Statement()
        {
            bool _isGround = player.sensor.ishit;
            if (originSO.isAirCheckMode)
            {
                if (_isGround == true)
                    return false;
                return true;
            }
            return _isGround;
        }
    }
}

