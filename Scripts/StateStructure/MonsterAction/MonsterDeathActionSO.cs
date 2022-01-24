using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;
namespace StateStructure
{

    [CreateAssetMenu(fileName = "MonsterDeathActionSO", menuName = "State Machines/Actions/MonsterDeathAction")]
    public class MonsterDeathActionSO : StateActionSO
    {
        public GameEvents gameEvents => _gameEventsSO;
        public bool isBoss => _isBoss;

        [SerializeField] GameEvents _gameEventsSO;
        [SerializeField] bool _isBoss;
        protected override StateAction CreateAction() => new MonsterDeathAction();
    }

    public class MonsterDeathAction : StateAction
    {
        private new MonsterDeathActionSO originSO => (MonsterDeathActionSO)base.OriginSO;

        MonsterActor monster;
        Animator anim;
        NavMeshAgent navAgent;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            anim = sm.GetComponent<Animator>();
        }

        string attackParam;
        public override void OnEnter()
        {
            navAgent.isStopped = true;
        }


        public override void OnExit()
        {
            if (!originSO.isBoss)
            {
                originSO.gameEvents.OnDeathMonsterEvent();
                Debug.Log("데스호출");
            }
            else
                originSO.gameEvents.OnDeathBossEvent();
                
            GameObject.Destroy(monster.gameObject);
        }

    }
}

