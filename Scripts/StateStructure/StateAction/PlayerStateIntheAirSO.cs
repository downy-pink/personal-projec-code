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


            //중력 값 적용
            _verticalM -= player.transform.up * originSO.gravity * Time.fixedDeltaTime;

            //운동량이 적용되지 않은 속도 값 가져오기.
            Vector3 _movementVelocity = player.SetnonMomentVelocity(originSO.moveMentSpeed.Variable, originSO.playerCameraTransform.Variable);

            //수평 운동량이 설정 속도보다 높으면 수평운동량을 조절한다.
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
            
            //공중에서도 마찰고려
            Vector3.MoveTowards(_horizontalM, Vector3.zero, originSO.airFriction * Time.fixedDeltaTime);
            player.momentum = _horizontalM + _verticalM;
        }

    }
}
