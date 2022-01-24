using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateStructure
{
    //���°� ���� �� ���� �� ���� �Ұ��ΰ�?
    [CreateAssetMenu(fileName = "IsStateStartDelayTimeSO", menuName = "State Machines/Conditions/IsStateStartDelayTime")]
    public class IsStateStartDelayTimeConditionSO : StateConditionSO<IsStartCondition>
    {
        public float timerLength = .5f;
        protected override Condition CreateCondition() => new IsStateStartDelayTimeCondition();
    }

    public class IsStateStartDelayTimeCondition : Condition
    {
        private IsStateStartDelayTimeConditionSO _originSO => (IsStateStartDelayTimeConditionSO)base.OriginSO;

        private float _startTime;

        public override void Awake(StateMachine _stateMachine)
        {
            _startTime = -_originSO.timerLength;
        }

        public override void OnExit()
        {
            _startTime = Time.time;
        }

        protected override bool Statement() => Time.time >= _startTime + _originSO.timerLength;
    }

}

