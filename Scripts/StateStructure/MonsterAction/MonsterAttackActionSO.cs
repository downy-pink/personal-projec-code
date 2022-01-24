using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterAttackActionSO", menuName = "State Machines/Actions/MonsterAttackAction")]
    public class MonsterAttackActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new MonsterAttackAction();
    }

    public class MonsterAttackAction : StateAction
    {
        private new MonsterAttackActionSO originSO => (MonsterAttackActionSO)base.OriginSO;

        MonsterActor monster;
        Animator anim;
        NavMeshAgent navAgent;
        Transform target;
        Attack attack;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            anim = sm.GetComponent<Animator>();
            attack = sm.GetComponent<Attack>();
        }

        string attackParam;
        public override void OnEnter()
        {
            attackParam = monster.AttackStringChange();
            if (anim!=null)
             anim.SetBool(attackParam, true);
            navAgent.isStopped = true;
            navAgent.speed = 0;
            monster.ActiveAttackEffect();
            monster.StartCoroutine(DoAttacktruetime());
            Debug.Log("공격");
        }


        public override void OnExit()
        {
            if (attack != null) //이거떄문에 버그생김
                attack.ColDisabled();
            navAgent.isStopped = false;
            if (anim != null)
                anim.SetBool(attackParam, false);
            monster.DeactiveAttackEffect();
            monster.isAttack = false;
        }

        IEnumerator DoAttacktruetime()
        {
            yield return new WaitForSeconds(0.4f);
            Debug.Log("isattack");
            monster.isAttack = true;
            yield return new WaitForSeconds(0.3f);
            monster.isAttack = false;
        }
    }
}

