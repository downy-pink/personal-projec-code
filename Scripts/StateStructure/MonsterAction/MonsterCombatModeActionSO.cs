using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Helper;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterCombatModeActionSO", menuName = "State Machines/Actions/MonsterCombatModeAction")]
    public class MonsterCombatModeActionSO : StateActionSO
    {

        public float playerDis => _playerDis;
        public float moveAngle => _moveAngle;
        public TransformValueSO playerT => _playerT;
        public float combatModeSpeed => _combatModeSpeed;

        [SerializeField] float _playerDis; //플레이어와 거리
        [SerializeField] float _moveAngle; //플레이어 중심으로 한번 이동 시 움직일 각도
        [SerializeField] TransformValueSO _playerT;
        [SerializeField] float _combatModeSpeed;
        protected override StateAction CreateAction() => new MonsterCombatModeAction();
    }

    public class MonsterCombatModeAction : StateAction
    {
        private new MonsterCombatModeActionSO originSO => (MonsterCombatModeActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;
        Animator anim;
        Attack attack;
        //int lorRSelectMove; //왼쪽 또는 오른쪽 이동 정하기
        //float applyMoveAngle; //적용할 이동 각도

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            anim = sm.GetComponent<Animator>();
            attack = sm.GetComponent<Attack>();
        }


        public override void OnEnter()
        {
            ////--------오른쪽 왼쪽이동 상태 코드-----------
            //Vector3 _fPlayertoMonster = (monster.transform.position - originSO.playerT.Variable.position).normalized;
            //applyMoveAngle = originSO.moveAngle;
            //lorRSelectMove = Random.Range(0, 2);
            //if (lorRSelectMove == 0) //오른쪽 각 이동
            //{
            //    applyMoveAngle *= -1;
            //    anim.SetBool("IsRigtCombatMode", true);
            //}
            //else //왼쪽 각 이동
            //{
            //    anim.SetBool("IsLeftCombatMode", true);
            //}
            //Quaternion _v3Rot = Quaternion.AngleAxis(applyMoveAngle, Vector3.up);

            //Vector3 des = (_v3Rot * _fPlayertoMonster * originSO.playerDis) + originSO.playerT.Variable.position; //이동할 목적지
            //navAgent.speed = originSO.combatModeSpeed;
            //navAgent.isStopped = false;
            //navAgent.SetDestination(des);

            navAgent.isStopped = false;
            Vector3 _fmtop = (originSO.playerT.Variable.position - monster.transform.position).normalized;
            navAgent.SetDestination(_fmtop*originSO.playerDis + originSO.playerT.Variable.position); //플레이어와 어느정도 떨어진거리 위치 지정
        }

        public override void OnUpdate()
        {
            monster.transform.LookAt(originSO.playerT.Variable);

        }

        public override void OnExit()
        {
            //if (lorRSelectMove == 0) //오른쪽 각 이동
            //{
            //    applyMoveAngle *= -1;
            //    anim.SetBool("IsRigtCombatMode", false);
            //}
            //else //왼쪽 각 이동
            //{
            //    anim.SetBool("IsLeftCombatMode", false);
            //}
            //navAgent.isStopped = true;
            if (attack != null) //이거떄문에 버그생김
                attack.ColDisabled();
            navAgent.isStopped = true;

        }
    }
}

