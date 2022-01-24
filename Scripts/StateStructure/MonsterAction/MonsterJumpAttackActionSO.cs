using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using Actors;
using DG.Tweening;
using SonVectorMath;
using Helper;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "MonsterJumpAttackActionSO", menuName = "State Machines/Actions/MonsterJumpAttackAction")]
    public class MonsterJumpAttackActionSO : StateActionSO
    {
        public GameObject jumpAttackprefab => _jumpAttackprefab;
        public float jumpAttackprefabInsTime => _jumpAttackprefabInsTime;
        public float jumpAttackprefabDestroyTime => _jumpAttackprefabDestroyTime;
        public GameObject jumpAttackPrefab => _jumpAttackPrefab;
        public TransformValueSO playerT => _playerT;

        [SerializeField] GameObject _jumpAttackprefab;
        [SerializeField] float _jumpAttackprefabInsTime;
        [SerializeField] float _jumpAttackprefabDestroyTime;
        [SerializeField] GameObject _jumpAttackPrefab;
        [SerializeField] TransformValueSO _playerT;
        protected override StateAction CreateAction() => new MonsterJumpAttackAction();
    }

    public class MonsterJumpAttackAction : StateAction
    {
        private new MonsterJumpAttackActionSO originSO => (MonsterJumpAttackActionSO)base.OriginSO;

        MonsterActor monster;
        Animator anim;
        NavMeshAgent navAgent;
        Transform target;

        float currentTime;
        bool isIns;

        public override void Awake(StateMachine sm)
        {
            monster = sm.GetComponent<MonsterActor>();
            navAgent = sm.GetComponent<NavMeshAgent>();
            anim = sm.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            Debug.Log("점프공격");
            navAgent.isStopped = true;
            navAgent.speed = 0;
            currentTime = 0;
            isIns = false;
            monster.transform.DOMove(monster.transform.position + monster.transform.forward * 8f, 1.2f);
            monster.StartCoroutine(DoJumpAttackCoroutine());
        }

        public override void OnUpdate()
        {
            currentTime += Time.deltaTime;

            if (currentTime >= originSO.jumpAttackprefabInsTime && !isIns)
            {
                isIns = true;
                GameObject _obj = GameObject.Instantiate(originSO.jumpAttackprefab, monster.transform.position,
                    Quaternion.identity);
                GameObject.Destroy(_obj, originSO.jumpAttackprefabDestroyTime);
            }
            Debug.DrawRay(monster.transform.position + monster.transform.up, -monster.transform.up,Color.red);
        }

        public override void OnExit()
        {
            monster.StopCoroutine(DoJumpAttackCoroutine());
            if (jumpAttackEffect != null)
                GameObject.Destroy(jumpAttackEffect);
        }
        GameObject jumpAttackEffect;
        IEnumerator DoJumpAttackCoroutine()
        {
            yield return new WaitForSeconds(1.3f);
            RaycastHit ray;
            int _layer = 1 << LayerMask.NameToLayer("Default");

            Physics.Raycast(monster.transform.position + monster.transform.up, -monster.transform.up, out ray, 7,_layer);
            Vector3 _projecGround = Vector3.ProjectOnPlane(monster.transform.up, ray.transform.up);
            Vector3 _insPos = new Vector3(monster.transform.position.x, 8, monster.transform.position.z);
            GameObject jumpAttackEffect = GameObject.Instantiate(originSO.jumpAttackPrefab, _insPos + monster.transform.forward * 1.5f,
                originSO.jumpAttackPrefab.transform.rotation);
            originSO.playerT.Variable.gameObject.GetComponent<PlayerActor>().TryCameraShake(0, 0.2f, 8.5f, 130);
            GameObject.Destroy(jumpAttackEffect, 8.2f);
        }
    }
}

