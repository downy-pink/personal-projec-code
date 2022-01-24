using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using SonVectorMath;

public class TurnTowardVelocity : MonoBehaviour
{
    public PlayerActor playeractor;

    //ĳ���Ͱ� ȸ���ϴ� �ӵ�
    public float turnSpeed = 500f;

    //���� ȸ���ؾ��ϴ� ��
    float currentYRot = 0f;

    //�ִ� �ޱ۰� ����ŭ 1�� ������� turnspeed���� ����� ������ ����
    float limitAngleSpeed = 90f;

    private void LateUpdate()
    {
        Vector3 _velocity;

        _velocity = playeractor.nonMomentVelocity + playeractor.momentum;

        //�ӵ����� ĳ���Ϳ� ������ ���� ������Ų��.
        _velocity = Vector3.ProjectOnPlane(_velocity, transform.parent.up);

        //�ӵ��� �ּҰ����Ϸ� �������� �۵� ����
        float _magnitudeMinThreshold = 0.001f;
        if (_velocity.magnitude < _magnitudeMinThreshold)
            return;

        _velocity.Normalize();

        float _angleDifference = VectorMath.GetAngle(transform.forward, _velocity, transform.parent.up);

        float _speedKey = Mathf.InverseLerp(0f, limitAngleSpeed, Mathf.Abs(_angleDifference)); //0�� limitAngleSpeed������ angleDifference������ġ

        float _step = Mathf.Sign(_angleDifference) * _speedKey * Time.deltaTime * turnSpeed;

        //�ִ� ���� �Ѿ��� �ִ밪���� ����
        if (_angleDifference < 0f && _step < _angleDifference)
            _step = _angleDifference;
        else if (_angleDifference > 0f && _step > _angleDifference)
            _step = _angleDifference;

        currentYRot += _step;

        //360 �ѱ�� Ŭ����
        if (currentYRot > 360f)
            currentYRot -= 360f;
        if (currentYRot < -360f)
            currentYRot += 360f;

        transform.localRotation = Quaternion.Euler(0f, currentYRot, 0f);
    }

}
