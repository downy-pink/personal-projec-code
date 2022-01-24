using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using SonVectorMath;

public class TurnTowardVelocity : MonoBehaviour
{
    public PlayerActor playeractor;

    //캐릭터가 회전하는 속도
    public float turnSpeed = 500f;

    //현재 회전해야하는 값
    float currentYRot = 0f;

    //최대 앵글과 가까운만큼 1로 가까워져 turnspeed값을 제대로 받을수 있음
    float limitAngleSpeed = 90f;

    private void LateUpdate()
    {
        Vector3 _velocity;

        _velocity = playeractor.nonMomentVelocity + playeractor.momentum;

        //속도값을 캐릭터와 수직인 땅에 투영시킨다.
        _velocity = Vector3.ProjectOnPlane(_velocity, transform.parent.up);

        //속도가 최소값이하로 떨어지면 작동 중지
        float _magnitudeMinThreshold = 0.001f;
        if (_velocity.magnitude < _magnitudeMinThreshold)
            return;

        _velocity.Normalize();

        float _angleDifference = VectorMath.GetAngle(transform.forward, _velocity, transform.parent.up);

        float _speedKey = Mathf.InverseLerp(0f, limitAngleSpeed, Mathf.Abs(_angleDifference)); //0과 limitAngleSpeed사이의 angleDifference현재위치

        float _step = Mathf.Sign(_angleDifference) * _speedKey * Time.deltaTime * turnSpeed;

        //최대 값을 넘어기면 최대값으로 고정
        if (_angleDifference < 0f && _step < _angleDifference)
            _step = _angleDifference;
        else if (_angleDifference > 0f && _step > _angleDifference)
            _step = _angleDifference;

        currentYRot += _step;

        //360 넘기면 클리핑
        if (currentYRot > 360f)
            currentYRot -= 360f;
        if (currentYRot < -360f)
            currentYRot += 360f;

        transform.localRotation = Quaternion.Euler(0f, currentYRot, 0f);
    }

}
