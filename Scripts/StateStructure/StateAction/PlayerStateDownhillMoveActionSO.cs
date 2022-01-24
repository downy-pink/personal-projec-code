using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using SonVectorMath;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerStateDownhillMoveAction", menuName = "State Machines/Actions/PlayerStateDownhillMoveAction")]
    public class PlayerStateDownhillMoveActionSO : StateActionSO
    {
        public float slideGravity => _slideGravity;

        [SerializeField] [Range(0f, 50f)] float _slideGravity = 25f;
        protected override StateAction CreateAction() => new PlayerStateDownhillMoveAction();
    }

    public class PlayerStateDownhillMoveAction : StateAction
    {
        private new PlayerStateDownhillMoveActionSO originSO => (PlayerStateDownhillMoveActionSO)base.OriginSO;

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

            //현재 운동량이 경사면에 미치는 힘만을 뽑아내기
            player.momentum = Vector3.ProjectOnPlane(player.momentum, player.GetGroundNormal());

            //수직 운동량으로 인해서 튀는 현상 방지
            if (VectorMath.GetVectorProjectionfloat(player.momentum, player.transform.up) > 0f)
                player.momentum = VectorMath.RemoveVector(player.momentum, player.transform.up);

            Vector3 _slideDirection = Vector3.ProjectOnPlane(-player.transform.up, player.GetGroundNormal()).normalized;
            player.momentum += _slideDirection * originSO.slideGravity * Time.fixedDeltaTime;
        }

        public override void OnExit()
        {
            player.IsSensorExtendedRange(false);
        }
    }
}

