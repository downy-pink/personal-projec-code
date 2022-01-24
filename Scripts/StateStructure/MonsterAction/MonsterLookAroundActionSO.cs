using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterLookAroundActionSO", menuName = "State Machines/Actions/MonsterLookAroundAction")]
    public class MonsterLookAroundActionSO : StateActionSO
    {
        public MonsterPatrolActionSO parolSO => _patrolSO;
        [SerializeField] MonsterPatrolActionSO _patrolSO;
        protected override StateAction CreateAction() => new MonsterLookAroundAction();
    }

    public class MonsterLookAroundAction : StateAction
    {
        private new MonsterLookAroundActionSO originSO => (MonsterLookAroundActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;
        Transform target;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
        }
        public override void OnExit()
        {
            
        }

    }
}

