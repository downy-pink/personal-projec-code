using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;


namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterHitActionSO", menuName = "State Machines/Actions/MonsterHitAction")]
    public class MonsterHitActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new MonsterHitAction();
    }

    public class MonsterHitAction : StateAction
    {
        private new MonsterHitActionSO originSO => (MonsterHitActionSO)base.OriginSO;

        MonsterActor monster;
        Animator anim;
        NavMeshAgent navAgent;
        Transform target;

        Attack attack;
        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            attack = sm.GetComponent<Attack>();
        }

        string attackParam;
        public override void OnEnter()
        {
            Debug.Log("¸ÂÀ½");
            navAgent.isStopped = true;
            navAgent.speed = 0;
        }


        public override void OnExit()
        {
            attack.ColDisabled();
        }

    }
}