using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;

namespace StateStructure
{
    //수평, 수직이동
    [CreateAssetMenu(fileName = "PlayerStateMoveAction", menuName = "State Machines/Actions/PlayerStateMoveAction")]
    public class PlayerStateMoveActionSO : StateActionSO
    {
        public float speed => _speed;
        public TransformValueSO playerCameraTransform;
        public FloatValue speedSO;
        
        [SerializeField] [Range(0.1f, 50f)] float _speed = 10f;

        protected override StateAction CreateAction() => new PlayerStateMoveAction();
    }

    public class PlayerStateMoveAction : StateAction
    {
        private new PlayerStateMoveActionSO originSO => (PlayerStateMoveActionSO)base.OriginSO;

        PlayerActor player;

        public override void Awake(StateMachine sm)
        {
            player = sm.GetComponent<PlayerActor>();
            originSO.speedSO.Variable = originSO.speed;
        }

        public override void OnFixedUpdate()
        {
            player.nonMomentVelocity = player.SetnonMomentVelocity(originSO.speed, originSO.playerCameraTransform.Variable);
        }

        public override void OnExit()
        {
            player.nonMomentVelocity = Vector3.zero;
        }
    }
}

