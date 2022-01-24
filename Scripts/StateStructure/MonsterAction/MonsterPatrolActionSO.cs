using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using UnityEngine.AI;
using Helper;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterPatrolAction", menuName = "State Machines/Actions/MonsterPatrolAction")]
    public class MonsterPatrolActionSO : StateActionSO
    {
        public float walkSpeed => _walkSpeed;
        [SerializeField] float _walkSpeed;
        protected override StateAction CreateAction() => new MonsterPatrolAction();
    }

    public class MonsterPatrolAction : StateAction
    {
        private new MonsterPatrolActionSO originSO => (MonsterPatrolActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;
        Transform target;
        int patrolArrayNum; //순회 위치

        public override void Awake(StateMachine sm)
        {
            Debug.Log("순회");
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            patrolArrayNum = Random.Range(0, monster.patrolT.Length);
      
        }

       
        public override void OnEnter()
        {
            navAgent.speed = originSO.walkSpeed;
            navAgent.isStopped = false;
            if (patrolArrayNum >= monster.patrolT.Length)
                patrolArrayNum = 0;
            target = monster.patrolT[patrolArrayNum++];
            navAgent.SetDestination(target.position);
            monster.DisabledCombat();
        }

        public override void OnExit()
        {
            navAgent.isStopped = true;
        }
    }
}

