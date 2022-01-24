using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    //상태에 진입했을 경우를 알려주는 클래스
    [CreateAssetMenu(fileName = "IsTriggerAnimatorParamConditionSO", menuName = "State Machines/Conditions/IsTrigger AnimatorParam Condition")]
    public class IsCurrentStateAnimatorParamConditionSO : StateConditionSO<IsCurrentStateAnimatorParamCondition>
    {
        public bool isReverseMode;
        public string paramName;
        public bool isAIMode;//ai일 경우 체크
        protected override Condition CreateCondition() => new IsCurrentStateAnimatorParamCondition();
    }

    public class IsCurrentStateAnimatorParamCondition : Condition
    {
        private IsCurrentStateAnimatorParamConditionSO originSO => (IsCurrentStateAnimatorParamConditionSO)base.OriginSO;
        Animator anim;
        MonsterActor monster;
        public override void Awake(StateMachine _stateMachine)
        {
            anim = _stateMachine.GetComponent<Animator>();
            if(originSO.isAIMode)
            monster = _stateMachine.GetComponent<MonsterActor>();
        }

        protected override bool Statement()
        {
            bool _isTrigger = false;
            if (originSO.isAIMode)
            {
                _isTrigger = anim.GetCurrentAnimatorStateInfo(0).IsName(monster.GetAttackString());
                if (originSO.isReverseMode)
                    return !_isTrigger;
                return _isTrigger;
            }

            _isTrigger = anim.GetCurrentAnimatorStateInfo(0).IsName(originSO.paramName);
            if (originSO.isReverseMode)
                return !_isTrigger;
            return _isTrigger;
        }
    }
}
