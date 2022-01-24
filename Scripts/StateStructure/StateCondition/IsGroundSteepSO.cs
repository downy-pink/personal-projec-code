using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    //�������ִ� �ִ� ��� �Ǵ�
    [CreateAssetMenu(fileName = "IsGroundSteep", menuName = "State Machines/Conditions/IsGroundSteep")]
    public class IsGroundSteepSO : StateConditionSO<IsGroundSteep>
    {
        public bool isNotSteepMode; //������ �ƴҰ�� Ʈ��
        [HideInInspector]public float slopeLimit => _slopeLimit;
        [SerializeField] float _slopeLimit = 80f;
        protected override Condition CreateCondition() => new IsGroundSteep();
    }

    public class IsGroundSteep : Condition
    {
        private IsGroundSteepSO originSO => (IsGroundSteepSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _stateMachine)
        {
            player = _stateMachine.GetComponent<PlayerActor>();
        }

        protected override bool Statement()
        {
            bool _isSteep = Vector3.Angle(player.GetGroundNormal(), player.transform.up) > originSO.slopeLimit;
            if (originSO.isNotSteepMode)
            {
                if (_isSteep)
                    return false;
                return true;
            }
            return _isSteep;
        }
    }
}

