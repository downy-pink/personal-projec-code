using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsPatrolReturnConditionSO", menuName = "State Machines/Conditions/IsPatrolReturnCondition")]
    public class IsPatrolReturnConditionSO : StateConditionSO<IsChaseDisCondion>
    {
        public TransformValueSO playerT => _playerT;
        public float patrolReuturnDis => _patrolReuturnDis;
        public float patrolCombatReturnDis => _patrolCombatReturnDis;

        [SerializeField] TransformValueSO _playerT;
        [SerializeField] float _patrolReuturnDis;
        [SerializeField] float _patrolCombatReturnDis;
        protected override Condition CreateCondition() => new IsPatrolReturnCondition();
    }

    public class IsPatrolReturnCondition : Condition
    {
        private IsPatrolReturnConditionSO originSO => (IsPatrolReturnConditionSO)base.OriginSO;

        MonsterActor monster;
        float applypatrolReturnDis;

        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();
            applypatrolReturnDis = originSO.patrolReuturnDis;
            monster.EnabledCombat += EnabledCombat;
            monster.DisabledCombat += DisabledCombat;
        }

        void EnabledCombat()
        {
            applypatrolReturnDis = originSO.patrolCombatReturnDis;
        }

        void DisabledCombat()
        {
            applypatrolReturnDis = originSO.patrolReuturnDis;
        }

        protected override bool Statement()
        {
            return Vector3.Distance(monster.transform.position, originSO.playerT.Variable.position) > applypatrolReturnDis;
        }
    }
}

