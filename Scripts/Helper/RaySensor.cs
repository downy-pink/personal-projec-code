using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    //레이를 더욱 쉽게 활용 할 수 있도록 하는 클래스
    public class RaySensor
    {
        //디버그 모드
        public bool debugMode;
        public float castLength = 1f;

        //적용 할 오브젝트 Transform
        Transform tr;
        Collider col;

        //인식 될 레이어
        LayerMask layer;

        //레이를 실제로 쏘는 곳
        Vector3 origin;

        //레이가 맞은 곳
        Vector3 hitPoint;
        //레이 맞은 곳의 노말 
        Vector3 hitNormal;
        //origin에서 맞은 곳까지 거리
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
