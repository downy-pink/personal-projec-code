using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using SonVectorMath;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerStateIntheGround", menuName = "State Machines/Actions/PlayerStateIntheGround")]
    public class PlayerStateIntheGroundSO : StateActionSO
    {
        public float groundFriction => _groundFriction;

        [SerializeField] [Range(0f, 120f)] float _groundFriction= 80; 
        protected override StateAction CreateAction() => new PlayerStateIntheGround();
    }

    public class PlayerStateIntheGround : StateAction
    {
        private new PlayerStateIntheGroundSO originSO => (PlayerStateIntheGroundSO)base.OriginSO;

        PlayerActor player;

        public override void Awake(StateMachine sm)
        {
            player = sm.GetComponent<PlayerActor>();
        }


        public override void OnEnter()
        {
            player.IsSensorExtendedRange(true);
            player.OnGroundContactLost();
        }

        public override void OnFixedUpdate()
        {
            //����� ���� ���� �������� ���� �ش�.
            Vector3 _verticalM = player.GetVerticalMomentum();
            Vector3 _horizontalM = player.GetHorizontalMomentum();
       
            //�߷� ���������ؼ� ����� ������ �� ��� ���
            if (VectorMath.GetVectorProjectionfloat(_verticalM, player.transform.up) < 0f)
                _verticalM = Vector3.zero;

            //�������
            _horizontalM = Vector3.MoveTowards(_horizontalM, Vector3.zero, originSO.groundFriction * Time.fixedDeltaTime);
            player.momentum = _horizontalM + _verticalM;
        }

        public override void OnExit()
        {
            player.IsSensorExtendedRange(false);
        }
    }
}
