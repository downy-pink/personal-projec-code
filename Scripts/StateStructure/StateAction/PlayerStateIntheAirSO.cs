using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;
using SonVectorMath;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerStateIntheAir", menuName = "State Machines/Actions/PlayerStateIntheAir")]
    public class PlayerStateIntheAirSO : StateActionSO
    {
        public float gravity => _gravity;
        public float airControlRate => _airControlRate;
        public float airFriction => _airFriction;
        public TransformValueSO playerCameraTransform;

        [SerializeField] [Range(0f, 4f)] float _airControlRate = 1.8f;
        [SerializeField] [Range(0f, 50f)] float _gravity = 40f;
        [SerializeField] [Range(0f, 1f)] float _airFriction = 0.35f;

        public FloatValue moveMentSpeed;
        protected override StateAction CreateAction() => new PlayerStateIntheAir();
    }


    public class PlayerStateIntheAir : StateAction
    {
        private new PlayerStateIntheAirSO originSO => (PlayerStateIntheAirSO)base.OriginSO;

        PlayerActor player;

        public override void Awake(StateMachine sm)
        {
            player = sm.GetComponent<PlayerActor>();
        }


        public override void OnEnter()
        {
            player.OnGroundContactLost();
        }

        public override void OnFixedUpdate()
        {
            Vector3 _verticalM = player.GetVerticalMomentum();
            Vector3 _horizontalM = player.GetHorizontalMomentum();


            //�߷� �� ����
            _verticalM -= player.transform.up * originSO.gravity * Time.fixedDeltaTime;

            //����� ������� ���� �ӵ� �� ��������.
            Vector3 _movementVelocity = player.SetnonMomentVelocity(originSO.moveMentSpeed.Variable, originSO.playerCameraTransform.Variable);

            //���� ����� ���� �ӵ����� ������ �������� �����Ѵ�.
            if (_horizontalM.magnitude > originSO.moveMentSpeed.Variable)
            {
                if (VectorMath.GetVectorProjectionfloat(_movementVelocity, _horizontalM.normalized) > 0)
                    _movementVelocity = VectorMath.RemoveVector(_movementVelocity, _horizontalM.normalized);

                float _airControlMul = 0.2f;
                _horizontalM += _movementVelocity * Time.fixedDeltaTime * originSO.airControlRate * _airControlMul;
            }

            else
            {
                _horizontalM += _movementVelocity * Time.fixedDeltaTime * originSO.airControlRate;
                _horizontalM = Vector3.ClampMagnitude(_horizontalM, originSO.moveMentSpeed.Variable);
            }
            
            //���߿����� �������
            Vector3.MoveTowards(_horizontalM, Vector3.zero, originSO.airFriction * Time.fixedDeltaTime);
            player.momentum = _horizontalM + _verticalM;
        }

    }
}
