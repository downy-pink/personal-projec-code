using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsTimeOut", menuName = "State Machines/Conditions/TimeElapse")]
    public class IsTimeElapseConditionSO : StateConditionSO<TimeElapsedCondition>
    {
        public float timerLength = .5f;
        protected override Condition CreateCondition() => new TimeElapsedCondition();
    }

    public class TimeElapsedCondition : Condition
    {
        private float _startTime;
        private IsTimeElapseConditionSO _originSO => (IsTimeElapseConditionSO)base.OriginSO; // The SO this Condition spawned from

        public override void OnEnter()
        {
            _startTime = Time.time;
        }

        protected override bool Statement() => Time.time >= _startTime + _originSO.timerLength;
    }

}
