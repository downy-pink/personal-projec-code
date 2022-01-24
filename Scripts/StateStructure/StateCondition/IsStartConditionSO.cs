using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsStartConditionSO", menuName = "State Machines/Conditions/IsStartCondition")]
    //true로 설정하여 상태에 바로 진입 가능하도록 하는 클래스
    public class IsStartConditionSO : StateConditionSO<IsStartCondition>
    {
        protected override Condition CreateCondition() => new IsStartCondition();
    }

    public class IsStartCondition : Condition
    {
        private IsStartConditionSO originSO => (IsStartConditionSO)base.OriginSO;


        protected override bool Statement()
        {
            return true;
        }
    }
}

