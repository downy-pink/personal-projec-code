using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    //���̸� ���� ���� Ȱ�� �� �� �ֵ��� �ϴ� Ŭ����
    public class RaySensor
    {
        //����� ���
        public bool debugMode;
        public float castLength = 1f;

        //���� �� ������Ʈ Transform
        Transform tr;
        Collider col;

        //�ν� �� ���̾�
        LayerMask layer;

        //���̸� ������ ��� ��
        Vector3 origin;

        //���̰� ���� ��
        Vector3 hitPoint;
        //���� ���� ���� �븻 
        Vector3 hitNormal;
        //origin���� ���� ������ �Ÿ�
        float hitDistance;

        
        public bool ishit;
        public RaySensor(Transform _tr, float _length, LayerMask _layer, Collider _col)
        {
            tr = _tr;
            castLength = _length;
            layer =  _layer;
            col = _col;
        }

        public void SetOrigin(Vector3 _origin)
        {
            origin = tr.InverseTransformPoint(_origin);
        }

        public void DrawRay()
        {
            if (debugMode && ishit)
            {
                Vector3 _worldOrigin = tr.TransformPoint(origin);
                Debug.DrawLine(_worldOrigin, _worldOrigin - tr.up * castLength, Color.red);
            }
              
        }

        public RaycastHit GetRay()
        {
            Vector3 _worldOrigin = tr.TransformPoint(origin);
            ishit = false;
            hitPoint = Vector3.zero;
            hitNormal = tr.up;
            RaycastHit _hit;
            hitDistance = 0;

            bool _ishit = Physics.Raycast(_worldOrigin, -tr.up, out _hit, castLength, layer, QueryTriggerInteraction.Ignore);
            ishit = _ishit;
            if (ishit)
            {
                hitPoint = _hit.point;
                hitNormal = _hit.normal;
                hitDistance = _hit.distance;
            }

            return _hit;
        }

        public Vector3 GetGroundNormal()
        {
            return hitNormal;
        }

        public float GetDistance()
        {
            return hitDistance;
        }
    }

}
