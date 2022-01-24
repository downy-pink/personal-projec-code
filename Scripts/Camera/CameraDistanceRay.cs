using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TagHolder;

namespace SonCamera
{
    //레이를 통하여 3인칭카메라의 거리조절
    public class CameraDistanceRay : MonoBehaviour
    {

        
        public Transform cameraTransform;
        //카메라의 바로위 부모 트랜스폼
        public Transform cameraTargetTransform;


        //Ignore Raycast를 빼고 사용하기위함
        public LayerMask layerMask = ~0;

        //Ignore Raycast를 편하게쓰기위함
        int ignoreRaycastLayer;

        //레이를 무시하고싶은 오브젝트(보통 플레이어 캐릭터만 들어감)
        public Collider[] ignoreList;

        //무시해야하는 오브젝트의 레이어 임시저장
        int[] ignoreListLayers;

        float currentDistance;

        //카메라가 오브젝트와 충돌인식범위를 더욱좋게한다.
        public float minimumDistanceFromObstacles = 0.1f;

        //카메라가 이동을 자연스럽게 하기위한 보간값
        public float smoothingFactor = 25f;


        void Start()
        {
            ignoreListLayers = new int[ignoreList.Length];


            ignoreRaycastLayer = LayerMask.NameToLayer(Layer.IGNORE_RAYCAST);

            //Ignore Raycast뺀 레이어마스크 사용
            if (layerMask == (layerMask | (1 << ignoreRaycastLayer)))
                layerMask ^= (1 << ignoreRaycastLayer);


            if (cameraTransform == null)
                Debug.LogWarning("카메라 트랜스폼이 할당되지 않았습니다.", this);

            if (cameraTargetTransform == null)
                Debug.LogWarning("카메라의 타겟이 할당되지 않았습니다.", this);

            if (cameraTransform == null || cameraTargetTransform == null)
            {
                this.enabled = false;
                return;
            }


            //카메라 중심축과 카메라간의 거리
            currentDistance = (cameraTargetTransform.position - transform.position).magnitude;
        }

        void LateUpdate()
        {

            //레이어 길이가 달라지면 재갱신
            if (ignoreListLayers.Length != ignoreList.Length)
                ignoreListLayers = new int[ignoreList.Length];


            for (int i = 0; i < ignoreList.Length; i++)
            {
                ignoreListLayers[i] = ignoreList[i].gameObject.layer;
                ignoreList[i].gameObject.layer = ignoreRaycastLayer;
            }

            float _distance = GetCameraDistance();

            //레이리셋
            for (int i = 0; i < ignoreList.Length; i++)
            {
                ignoreList[i].gameObject.layer = ignoreListLayers[i];
            }

           
            currentDistance = Mathf.Lerp(currentDistance, _distance, Time.deltaTime * smoothingFactor);

            //최종적인 카메라 이동
            cameraTransform.position = transform.position + (cameraTargetTransform.position - transform.position).normalized * currentDistance;

        }

        
        float GetCameraDistance()
        {
            RaycastHit _hit;

            //카메라 타겟트랜스폼이 움직이는게 아니고 카메라가 움직이므로 항상 고정거리를 반환
            Vector3 _castDirection = cameraTargetTransform.position - transform.position;

                if (Physics.Raycast(new Ray(transform.position, _castDirection), out _hit, _castDirection.magnitude + minimumDistanceFromObstacles, layerMask, QueryTriggerInteraction.Ignore))
                {
                    //음수가될경우 카메라 위치가이상해지므로 조정.
                    if (_hit.distance - minimumDistanceFromObstacles < 0f)
                        return _hit.distance;
                    else
                        return _hit.distance - minimumDistanceFromObstacles;
                }

            return _castDirection.magnitude;
        }

    }
}

