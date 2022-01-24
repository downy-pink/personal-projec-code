using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonVectorMath
{
    public class VectorMath : MonoBehaviour
    {
        public static float GetAngle(Vector3 _v1, Vector3 _v2, Vector3 _normalStandard)
        {
            //Calculate angle and sign;
            float _angle = Vector3.Angle(_v1, _v2);
            float _sign = Mathf.Sign(Vector3.Dot(_normalStandard, Vector3.Cross(_v1, _v2)));

            //Combine angle and sign;
            float _signedAngle = _angle * _sign;

            return _signedAngle;
        }

        //���͸� ������Ų ���̸� �̾Ƴ�
        public static float GetVectorProjectionfloat(Vector3 _vector, Vector3 _dir)
        {
            //����ȭ ���־������ ���� ����� ��Ȯ�� ����
            if (_dir.sqrMagnitude != 1)
                _dir.Normalize();

            float _length = Vector3.Dot(_vector, _dir);

            return _length;
        }

        public static Vector3 RemoveVector(Vector3 _vector, Vector3 _dir)
        {
            
            if (_dir.sqrMagnitude != 1)
                _dir.Normalize();

            float _amount = Vector3.Dot(_vector, _dir);

            _vector -= _dir * _amount;

            return _vector;
        }

        public static Vector3 VectorProjection(Vector3 _vector, Vector3 _dir)
        {
            //����ȭ �������� ���� ������ ���
            if (_dir.sqrMagnitude != 1)
                _dir.Normalize();

            float _amount = Vector3.Dot(_vector, _dir);

            return _dir * _amount;
        }

    }
}

