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

        [SerializeField] float _playerDis; //�÷��̾�� �Ÿ�
        [SerializeField] float _moveAngle; //�÷��̾� �߽����� �ѹ� �̵� �� ������ ����
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
        //int lorRSelectMove; //���� �Ǵ� ������ �̵� ���ϱ�
        //float applyMoveAngle; //������ �̵� ����

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            anim = sm.GetComponent<Animator>();
            attack = sm.GetComponent<Attack>();
        }


        public override void OnEnter()
        {
            ////--------������ �����̵� ���� �ڵ�-----------
            //Vector3 _fPlayertoMonster = (monster.transform.position - originSO.playerT.Variable.position).normalized;
            //applyMoveAngle = originSO.moveAngle;
            //lorRSelectMove = Random.Range(0, 2);
            //if (lorRSelectMove == 0) //������ �� �̵�
            //{
            //    applyMoveAngle *= -1;
            //    anim.SetBool("IsRigtCombatMode", true);
            //}
            //else //���� �� �̵�
            //{
            //    anim.SetBool("IsLeftCombatMode", true);
            //}
            //Quaternion _v3Rot = Quaternion.AngleAxis(applyMoveAngle, Vector3.up);

            //Vector3 des = (_v3Rot * _fPlayertoMonster * originSO.playerDis) + originSO.playerT.Variable.position; //�̵��� ������
            //navAgent.speed = originSO.combatModeSpeed;
            //navAgent.isStopped = false;
            //navAgent.SetDestination(des);

            navAgent.isStopped = false;
            Vector3 _fmtop = (originSO.playerT.Variable.position - monster.transform.position).normalized;
            navAgent.SetDestination(_fmtop*originSO.playerDis + originSO.playerT.Variable.position); //�÷��̾�� ������� �������Ÿ� ��ġ ����
        }

        public override void OnUpdate()
        {
            monster.transform.LookAt(originSO.playerT.Variable);

        }

        public override void OnExit()
        {
            //if (lorRSelectMove == 0) //������ �� �̵�
            //{
            //    applyMoveAngle *= -1;
            //    anim.SetBool("IsRigtCombatMode", false);
            //}
            //else //���� �� �̵�
            //{
            //    anim.SetBool("IsLeftCombatMode", false);
            //}
            //navAgent.isStopped = true;
            if (attack != null) //�̰ŋ����� ���׻���
                attack.ColDisabled();
            navAgent.isStopped = true;

        }
    }
}

