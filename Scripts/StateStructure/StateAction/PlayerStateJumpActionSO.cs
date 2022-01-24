using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
   
    [CreateAssetMenu(fileName = "PlayerStateJumpAction", menuName = "State Machines/Actions/PlayerStateJumpAction")]
    public class PlayerStateJumpActionSO : StateActionSO
    {
        public float jumpSpeed => _jumpSpeed;
        [SerializeField] [Range(0.1f, 50f)] float _jumpSpeed = 15f;

        protected override StateAction CreateAction() => new PlayerStateJumpAction();
    }

    public class PlayerStateJumpAction : StateAction
    {
        private new PlayerStateJumpActionSO originSO => (PlayerStateJumpActionSO)base.OriginSO;

        PlayerActor player;
        public override void Awake(StateMachine _sm)
        {
            player = _sm.GetComponent<PlayerActor>();
        }


        public override void OnEnter()
        {
            player.OnGroundContactLost();
            player.IsSensorExtendedRange(false);
        }

        public override void OnFixedUpdate()
        {
            player.momentum = SonVectorMath.VectorMath.RemoveVector(player.momentum, player.transform.up);
            player.momentum += player.transform.up * originSO.jumpSpeed;
            //Vector3 _verticalM = Vector3.zero; 
            //_verticalM += player.transform.up * originSO.jumpSpeed;
            //player.momentum.y = _verticalM.y;
        }

    }
    
    
}
