using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsChaseDisCondionSO", menuName = "State Machines/Conditions/IsChaseDisCondion")]
    //플레이어와 몬스터간 거리 파악
    public class IsChaseDisCondionSO : StateConditionSO<IsChaseDisCondion>
    {
        public TransformValueSO playerT => _playerT;
        public float chaseDis => _chaseDis;
        public float combatChaseDis => _combatChaseDis;
        [SerializeField] TransformValueSO _playerT;
        [SerializeField] float _chaseDis;
        [SerializeField] float _combatChaseDis;
        protected override Condition CreateCondition() => new IsChaseDisCondion();
    }

    public class IsChaseDisCondion : Condition
    {
        private IsChaseDisCondionSO originSO => (IsChaseDisCondionSO)base.OriginSO;

        MonsterActor monster;
        float applyChaseDis;

        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();
            monster.EnabledCombat += EnabledCombat;
            monster.DisabledCombat += DisabledCombat;
            applyChaseDis = originSO.chaseDis;
        }

        void EnabledCombat()
        {
            applyChaseDis = originSO.combatChaseDis;
        }

        void DisabledCombat()
        {
            applyChaseDis = originSO.chaseDis;
        }

        protected override bool Statement()
        {
            return Vector3.Distance(monster.transform.position, originSO.playerT.Variable.position) <= applyChaseDis;
        }
    }
}

