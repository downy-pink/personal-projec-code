using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterDistanceAttackActionSO", menuName = "State Machines/Actions/MonsterDistanceAttackAction")]
    public class MonsterDistanceAttackActionSO : StateActionSO
    {
        public GameObject attackobj => _attackobj; //원거리 공격 오브젝트
        public float insDelayTime => _insDelayTime; //언제쯤 생성 시킬 것인가?
        public GameObject distanceCircleEffect => _distanceCircleEffect;

        [SerializeField] GameObject _attackobj;
        [SerializeField] float _insDelayTime;
        [SerializeField] GameObject _distanceCircleEffect;

        protected override StateAction CreateAction() => new MonsterDistanceAttackAction();
    }
    public class MonsterDistanceAttackAction : StateAction
    {
        private new MonsterDistanceAttackActionSO originSO => (MonsterDistanceAttackActionSO)base.OriginSO;

        MonsterActor monster;
        NavMeshAgent navAgent;

        float currentTime;
        bool isFlag;
        bool isIns;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
        }

        public override void OnEnter()
        {
            Debug.Log("원거리공격");
            currentTime = 0;
            isFlag = false;
            navAgent.isStopped = true;
            navAgent.speed = 0;
            monster.ActiveComboAttackEffect();
            monster.StartCoroutine(DoCircleEffect());
        }

        public override void OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= originSO.insDelayTime && !isFlag)
            {
                Quaternion _q = monster.transform.rotation;
                _q.x = Quaternion.identity.x;
                _q.z = Quaternion.identity.z;

                GameObject _distanceObj = GameObject.Instantiate(originSO.attackobj,
                    monster.transform.position  + monster.transform.up*2.1f, _q);
                GameObject.Destroy(_distanceObj, 10f);
                isFlag = true;
            }
        }

        public override void OnExit()
        {
            monster.DeactiveComboAttackEffect();
            if (circleEffect != null)
                GameObject.Destroy(circleEffect);
        }

        GameObject circleEffect;
        IEnumerator DoCircleEffect()
        {
            yield return new WaitForSeconds(0.4f);
            GameObject circleEffect = GameObject.Instantiate(originSO.distanceCircleEffect, monster.transform.position + monster.transform.up*1.2f,
                originSO.distanceCircleEffect.transform.rotation);
            GameObject.Destroy(circleEffect, 1);
        }
    }
}
