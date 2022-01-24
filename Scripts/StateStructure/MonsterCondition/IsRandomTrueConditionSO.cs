using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsRandomTrueConditionSO", menuName = "State Machines/Conditions/IsRandomTrueCondition")]
    public class IsRandomTrueConditionSO : StateConditionSO<IsHitCondition>
    {
        public int trueRandomPercent => _trueRandomPercent;
        public float checkTime => _checkTime;

        [SerializeField] int _trueRandomPercent;
        [SerializeField] float _checkTime; //랜덤을 언제 마다 체크 할 것인가?
        protected override Condition CreateCondition() => new IsRandomTrueCondition();
    }

    public class IsRandomTrueCondition : Condition
    {
        private IsRandomTrueConditionSO originSO => (IsRandomTrueConditionSO)base.OriginSO;
        float currentTime;

        public override void OnEnter()
        {
            currentTime = 0;
        }

        protected override bool Statement()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= originSO.checkTime)
            {
                return Random.Range(0, 1000) <= originSO.trueRandomPercent*10;
            }
            return false;

        }
    }
}

