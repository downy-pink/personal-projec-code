using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;


namespace StateStructure
{
    [CreateAssetMenu(fileName = "BossComboAttackActionSO", menuName = "State Machines/Actions/BossComboAttackAction")]
    public class BossComboAttackActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new BossComboAttackAction();
    }

    public class BossComboAttackAction : StateAction
    {
        private new BossComboAttackActionSO originSO => (BossComboAttackActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;
        Transform target;
        Attack attack;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            attack = sm.GetComponent<Attack>();
        }

        public override void OnEnter()
        {
            Debug.Log("ÄÞº¸°ø°Ý");
            navAgent.isStopped = true;
            navAgent.speed = 0;
            monster.ActiveComboAttackEffect();
        }

        public override void OnExit()
        {
            if (attack != null)
                attack.ColDisabled();
            //navAgent.isStopped = false;
            monster.DeactiveComboAttackEffect();
            monster.isAttack = false;
        }


        IEnumerator DoAttacktruetime()
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("isattack");
            monster.isAttack = true;
            yield return new WaitForSeconds(0.5f);
            monster.isAttack = false;
        }
    }
}

