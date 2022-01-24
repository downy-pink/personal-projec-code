using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterBlockActionSO", menuName = "State Machines/Actions/MonsterBlockAction")]
    public class MonsterBlockActionSO : StateActionSO
    {
        public float staminaConsume => _staminaConsume;

        [SerializeField] float _staminaConsume;
        protected override StateAction CreateAction() => new MonsterBlockAction();
    }

    public class MonsterBlockAction : StateAction
    {
        private new MonsterBlockActionSO originSO => (MonsterBlockActionSO)base.OriginSO;

        Stats stats;
        Actor actor;
        NavMeshAgent navAgent;

        public override void Awake(StateMachine _sm)
        {
            stats = _sm.GetComponent<Stats>();
            actor = _sm.GetComponent<Actor>();
            navAgent = _sm.GetComponent<NavMeshAgent>();
        }
      
        public override void OnEnter()
        {
            navAgent.isStopped = true;
            navAgent.speed = 0;
            stats.staminaConsume(originSO.staminaConsume);
            actor.SetBlock(true);
            Debug.Log("ºí¶ôµé¿È");
        }


        public override void OnExit()
        {
            actor.SetBlock(false);
        }


    }
}

