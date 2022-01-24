using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using Actors;

namespace StateStructure
{
    //공격 대상인 플레이어와 얼마나 가까이 있는가
    [CreateAssetMenu(fileName = "IsAttackTargetConditionSO", menuName = "State Machines/Conditions/IsAttackTargetCondition")]
    public class IsAttackTargetConditionSO : StateConditionSO<IsStartCondition>
    {
        //목적지 도달한 판정의 길이
        public float targetDis => _targetDis;
        public TransformValueSO playerT => _playerT;

        [SerializeField] float _targetDis;
        [SerializeField] TransformValueSO _playerT;
        protected override Condition CreateCondition() => new IsAttackTargetCondition();
    }

    public class IsAttackTargetCondition : Condition
    {
        private IsAttackTargetConditionSO originSO => (IsAttackTargetConditionSO)base.OriginSO;

        MonsterActor monster;
        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();
        }


        protected override bool Statement()
        {
            return Vector3.Distance(monster.transform.position, originSO.playerT.Variable.position) <= originSO.targetDis;
        }
    }
}

