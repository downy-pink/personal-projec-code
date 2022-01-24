using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Moment = StateStructure.StateAction.SpecificMoment;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "AnimatorMoveSpeedActionSO", menuName = "State Machines/Actions/Set Animator Move Speed")]
    public class AnimatorMoveSpeedActionSO : StateActionSO
    {
        public string parameterName = default;

        protected override StateAction CreateAction() => new AnimatorMoveSpeedAction(Animator.StringToHash(parameterName));
    }

    public class AnimatorMoveSpeedAction : StateAction
    {
        //Component references
        private Animator animator;
        private PlayerActor player;

        private AnimatorParameterActionSO originSO => (AnimatorParameterActionSO)base.OriginSO; // The SO this StateAction spawned from
        private int parameterHash;

        public AnimatorMoveSpeedAction(int _parameterHash)
        {
            parameterHash = _parameterHash;
        }

        public override void Awake(StateMachine stateMachine)
        {
            animator = stateMachine.GetComponent<Animator>();
            player = stateMachine.GetComponent<PlayerActor>();
        }

        public override void OnUpdate()
        {
            float normalisedSpeed = player.nonMomentVelocity.magnitude;
            animator.SetFloat(parameterHash, normalisedSpeed);

        }
    }


}
