using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsStartConditionSO", menuName = "State Machines/Conditions/IsStartCondition")]
    //true�� �����Ͽ� ���¿� �ٷ� ���� �����ϵ��� �ϴ� Ŭ����
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

