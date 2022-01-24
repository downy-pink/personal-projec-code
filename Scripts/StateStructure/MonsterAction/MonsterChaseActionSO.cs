using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using UnityEngine.AI;
using Helper;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterChaseActionSO", menuName = "State Machines/Actions/MonsterChaseAction")]
    public class MonsterChaseActionSO : StateActionSO
    {
        public float runSpeed => _runSpeed;
        public TransformValueSO playerTValue => _playerTValue;
        public float renewDes => _renewDes;

        [SerializeField] float _runSpeed;
        [SerializeField] TransformValueSO _playerTValue;
        [SerializeField] float _renewDes; //얼마마다 목적지를 갱신할 것인가?
        protected override StateAction CreateAction() => new MonsterChaseAction();
    }

    public class MonsterChaseAction : StateAction
    {
        private new MonsterChaseActionSO originSO => (MonsterChaseActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
        }


        public override void OnEnter()
        {
            Debug.Log("쫓기");
            navAgent.speed = originSO.runSpeed;
            navAgent.isStopped = false;
            Vector3 des = originSO.playerTValue.Variable.position + (monster.transform.position - originSO.playerTValue.Variable.position).normalized*0.35f;
            navAgent.SetDestination(des);
            currentTime = 0;
            monster.EnabledCombat(); //전투 활성화
        }

        float currentTime;
        public override void OnUpdate()
        {
            currentTime += Time.deltaTime;
            if(currentTime >= originSO.renewDes)
            {
                navAgent.SetDestination(originSO.playerTValue.Variable.position);
                currentTime = 0;
            }
        }

        public override void OnExit()
        {
            navAgent.isStopped = true;
            navAgent.speed = 0;


        }
    }
}

