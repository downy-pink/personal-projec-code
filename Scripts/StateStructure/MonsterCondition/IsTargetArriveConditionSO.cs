using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;
using UnityEngine.AI;

namespace StateStructure
{
    //ai 목적지와 얼마나 가까워져있는지 판정
    [CreateAssetMenu(fileName = "IsTargetArriveConditionSO", menuName = "State Machines/Conditions/IsTargetArriveCondition")]
    public class IsTargetArriveConditionSO : StateConditionSO<IsStartCondition>
    {
        //목적지 도달한 판정의 길이
        public float target => _target;

        [SerializeField] float _target;
        protected override Condition CreateCondition() => new IsTargetArriveCondition();
    }

    public class IsTargetArriveCondition : Condition
    {
        private IsTargetArriveConditionSO originSO => (IsTargetArriveConditionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;
        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();
            navAgent = _stateMachine.GetComponent<NavMeshAgent>();
        }


        protected override bool Statement()
        {
            return Vector3.Distance(monster.transform.position, navAgent.destination) <= originSO.target;
        }
    }
}

