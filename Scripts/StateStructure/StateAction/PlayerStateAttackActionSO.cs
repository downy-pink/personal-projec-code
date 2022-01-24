using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using input;
using SonCamera;
using SonVectorMath;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerStateAttackActionSO", menuName = "State Machines/Actions/PlayerState Attack Action")]
    public class PlayerStateAttackActionSO : StateActionSO
    {
        public AnimatorParameterActionSO attackAnimParam => _attackAnimParam;
        public AnimatorParameterActionSO attackOutAnimParam => _attackOutAnimParam;
        public string []attackParamsString => _attackParamsString;
        public IsCurrentStateAnimatorParamConditionSO isAttackTriggerConditionSO => _isAttackTriggerConditionSO;
        public InputBroadCaster inputBC => _inputBC;

        [SerializeField] InputBroadCaster _inputBC;
        [SerializeField] AnimatorParameterActionSO _attackAnimParam;
        [SerializeField] AnimatorParameterActionSO _attackOutAnimParam; //애니메이션 나갈 때 파라미터 값을 초기화 시켜주기 위함
        [SerializeField] string[] _attackParamsString;
        [SerializeField] IsCurrentStateAnimatorParamConditionSO _isAttackTriggerConditionSO;

        protected override StateAction CreateAction() => new PlayerStateAttackAction();
    }

    public class PlayerStateAttackAction : StateAction
    {
        private new PlayerStateAttackActionSO originSO => (PlayerStateAttackActionSO)base.OriginSO;

        PlayerActor player;
        Attack attack;
        //CameraDistanceRay cameraDis;
        //Coroutine attackCoroutine;
        //TurnTowardVelocity modelroot;
        int paramNum = -1; //공격 애니메이션 개수
        public override void Awake(StateMachine _sm)
        {
            player = _sm.GetComponent<PlayerActor>();
            attack = _sm.GetComponent<Attack>();
            //cameraDis = _sm.GetComponentInChildren<CameraDistanceRay>();
            //modelroot = _sm.GetComponentInChildren<TurnTowardVelocity>();
            if (originSO.attackParamsString.Length > 0)
            {
                originSO.attackAnimParam.parameterName = originSO.attackParamsString[0];
                originSO.attackOutAnimParam.parameterName = originSO.attackParamsString[0];
                originSO.isAttackTriggerConditionSO.paramName = originSO.attackParamsString[0];
            }
        }


        public override void OnEnter()
        {
            //player.StartCoroutine(AttackCoroutine());


            originSO.inputBC.OnAttack(false);
            paramNum++;
            if (paramNum >= originSO.attackParamsString.Length)
                paramNum = 0;
            originSO.attackAnimParam.parameterName = originSO.attackParamsString[paramNum];
            originSO.attackOutAnimParam.parameterName = originSO.attackParamsString[paramNum];
            originSO.isAttackTriggerConditionSO.paramName = originSO.attackParamsString[paramNum];
            player.StartCoroutine(SwordTrailDelayCoroutine());
        }

        public override void OnExit()
        {
            originSO.inputBC.OnAttack(false);
            attack.ColDisabled();
        }

        IEnumerator SwordTrailDelayCoroutine()
        {
            yield return new WaitForSeconds(0.15f);
            player.ActiveSwordStandardTrail();
            yield return new WaitForSeconds(0.55f);
            player.DeactiveSwordStandardTrail();
        }

        //float currentYRot;
        //IEnumerator AttackCoroutine()
        //{
        //    float _currentT = 0;
        //    Vector3 _look = modelroot.transform.position - cameraDis.cameraTransform.position;
        //    _look = VectorMath.RemoveVector(_look, Vector3.up);
        //    while (_currentT <= 0.1f)
        //    {
        //        _currentT += Time.time;
        //        Vector3 _lookForward = Vector3.Lerp(player.transform.forward, _look.normalized, Time.time*10);

        //        //float _angleDifference = VectorMath.GetAngle(player.transform.forward, cameraDis.cameraTransform.forward, player.transform.up);
        //        //float _speedKey = Mathf.InverseLerp(0f, 90, Mathf.Abs(_angleDifference)); //0과 limitAngleSpeed사이의 angleDifference현재위치
        //        //float _step = _angleDifference * Time.time * 1500 * _speedKey;

        //        //if (_angleDifference < 0f && _step < _angleDifference)
        //        //    _step = _angleDifference;
        //        //else if (_angleDifference > 0f && _step > _angleDifference)
        //        //    _step = _angleDifference;

        //        //currentYRot += _step;

        //        ////360 넘기면 클리핑
        //        //if (currentYRot > 360f)
        //        //    currentYRot -= 360f;
        //        //if (currentYRot < -360f)
        //        //    currentYRot += 360f;

        //        //modelroot.transform.rotation = Quaternion.Euler(0f, currentYRot, 0f);

        //        Quaternion _rot = Quaternion.LookRotation(_look.normalized);
        //        modelroot.transform.rotation = _rot;
        //    }
        //    originSO.inputBC.OnAttack(false);
        //    paramNum++;
        //    if (paramNum >= originSO.attackParamsString.Length)
        //        paramNum = 0;
        //    originSO.attackAnimParam.parameterName = originSO.attackParamsString[paramNum];
        //    originSO.attackOutAnimParam.parameterName = originSO.attackParamsString[paramNum];
        //    originSO.isAttackTriggerConditionSO.paramName = originSO.attackParamsString[paramNum];
        //    yield return null;
        //}

    }
}
