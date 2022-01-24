using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Core;
using Actors;
using Config;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterEvaisionActionSO", menuName = "State Machines/Actions/MonsterEvaisionAction")]
    public class MonsterEvaisionActionSO : StateActionSO
    {
        public float staminaConsume => staminaConsumeConfigSO.evasionConsume;
        public float backMoveDis => _backMoveDis;
        //이동시 걸리는 시간
        public float duration => _duration;
        public Ease ease => _ease;
        

        [SerializeField] float _backMoveDis;
        [SerializeField] float _duration;
        [SerializeField] Ease _ease;
        [SerializeField] EvasionStaminaConsumeConfigSO staminaConsumeConfigSO;
        protected override StateAction CreateAction() => new MonsterEvaisionAction();
    }

    public class MonsterEvaisionAction : StateAction
    {
        private new MonsterEvaisionActionSO originSO => (MonsterEvaisionActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;
        Stats stats;
        bool moveFlag; //뒤로이동 했는가?
        float currentbackMoveDelayT;
        Sequence _s;
        public override void Awake(StateMachine _sm)
        {
            monster = _sm.GetComponent<MonsterActor>();
            stats = _sm.GetComponent<Stats>();
            navAgent = _sm.GetComponent<NavMeshAgent>();
            _s = DOTween.Sequence();
        }


        public override void OnEnter()
        {
            Debug.Log("회피");
            navAgent.isStopped = true;
            navAgent.speed = 0;
            stats.staminaConsume(originSO.staminaConsume);
            currentbackMoveDelayT = 0;
            moveFlag = false;
            monster.SetBlock(true);
            monster.isEvasion = true;
        }

        public override void OnUpdate()
        {
            currentbackMoveDelayT += Time.deltaTime;
            NavMeshHit _hit = new NavMeshHit();
            if (monster.movePoint(monster.transform.position - (monster.transform.forward * originSO.backMoveDis), out _hit)
                && currentbackMoveDelayT >= 0.15f && !moveFlag)
            {
                moveFlag = true;
                _s
                .Append(monster.transform.DOMove(_hit.position, originSO.duration)).SetEase(originSO.ease);
            }
        }

        public override void OnExit()
        {
            monster.SetBlock(false);
            monster.isEvasion = false;
        }



    }
}

