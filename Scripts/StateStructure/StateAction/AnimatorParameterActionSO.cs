using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Moment = StateStructure.StateAction.SpecificMoment;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "AnimParamActionSO", menuName = "State Machines/Actions/Set Animator Parameter")]
    public class AnimatorParameterActionSO : StateActionSO
    {
        public ParameterType parameterType = default;
        public string parameterName = default;

        public bool boolValue = default;
        public int intValue = default;
        public float floatValue = default;

        public Moment whenToRun = default; // Allows this StateActionSO type to be reused for all 3 state moments

        protected override StateAction CreateAction() => new AnimatorParameterAction(Animator.StringToHash(parameterName));

        public enum ParameterType
        {
            Bool, Int, Float, Trigger,
        }
    }

    public class AnimatorParameterAction : StateAction
    {
        //Component references
        private Animator animator;
        private AnimatorParameterActionSO originSO => (AnimatorParameterActionSO)base.OriginSO; // The SO this StateAction spawned from
        private int parameterHash;

        public AnimatorParameterAction(int _parameterHash)
        {
            parameterHash = _parameterHash;
        }

        public override void Awake(StateMachine stateMachine)
        {
            animator = stateMachine.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            parameterHash = Animator.StringToHash(originSO.parameterName);
            if (originSO.whenToRun == SpecificMoment.OnEnter)
                SetParameter();
        }

        public override void OnExit()
        {
            if (originSO.whenToRun == SpecificMoment.OnExit)
                SetParameter();
        }

        private void SetParameter()
        {
            switch (originSO.parameterType)
            {
                case AnimatorParameterActionSO.ParameterType.Bool:
                    animator.SetBool(parameterHash, originSO.boolValue);
                    break;
                case AnimatorParameterActionSO.ParameterType.Int:
                    animator.SetInteger(parameterHash, originSO.intValue);
                    break;
                case AnimatorParameterActionSO.ParameterType.Float:
                    animator.SetFloat(parameterHash, originSO.floatValue);
                    break;
                case AnimatorParameterActionSO.ParameterType.Trigger:
                    animator.SetTrigger(parameterHash);
                    break;
            }
        }

    }

}

