using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TagHolder;

namespace SonCamera
{
    //���̸� ���Ͽ� 3��Īī�޶��� �Ÿ�����
    public class CameraDistanceRay : MonoBehaviour
    {

        
        public Transform cameraTransform;
        //ī�޶��� �ٷ��� �θ� Ʈ������
        public Transform cameraTargetTransform;


        //Ignore Raycast�� ���� ����ϱ�����
        public LayerMask layerMask = ~0;

        //Ignore Raycast�� ���ϰԾ�������
        int ignoreRaycastLayer;

        //���̸� �����ϰ���� ������Ʈ(���� �÷��̾� ĳ���͸� ��)
        public Collider[] ignoreList;

        //�����ؾ��ϴ� ������Ʈ�� ���̾� �ӽ�����
        int[] ignoreListLayers;

        float currentDistance;

        //ī�޶� ������Ʈ�� �浹�νĹ����� ���������Ѵ�.
        public float minimumDistanceFromObstacles = 0.1f;

        //ī�޶� �̵��� �ڿ������� �ϱ����� ������
        public float smoothingFactor = 25f;


        void Start()
        {
            ignoreListLayers = new int[ignoreList.Length];


            ignoreRaycastLayer = LayerMask.NameToLayer(Layer.IGNORE_RAYCAST);

            //Ignore Raycast�� ���̾��ũ ���
            if (layerMask == (layerMask | (1 << ignoreRaycastLayer)))
                layerMask ^= (1 << ignoreRaycastLayer);


            if (cameraTransform == null)
                Debug.LogWarning("ī�޶� Ʈ�������� �Ҵ���� �ʾҽ��ϴ�.", this);

            if (cameraTargetTransform == null)
                Debug.LogWarning("ī�޶��� Ÿ���� �Ҵ���� �ʾҽ��ϴ�.", this);

            if (cameraTransform == null || cameraTargetTransform == null)
            {
                this.enabled = false;
                return;
            }


            //ī�޶� �߽���� ī�޶��� �Ÿ�
            currentDistance = (cameraTargetTransform.position - transform.position).magnitude;
        }

        void LateUpdate()
        {

            //���̾� ���̰� �޶����� �簻��
            if (ignoreListLayers.Length != ignoreList.Length)
                ignoreListLayers = new int[ignoreList.Length];


            for (int i = 0; i < ignoreList.Length; i++)
            {
                ignoreListLayers[i] = ignoreList[i].gameObject.layer;
                ignoreList[i].gameObject.layer = ignoreRaycastLayer;
            }

            float _distance = GetCameraDistance();

            //���̸���
            for (int i = 0; i < ignoreList.Length; i++)
            {
                ignoreList[i].gameObject.layer = ignoreListLayers[i];
            }

           
            currentDistance = Mathf.Lerp(currentDistance, _distance, Time.deltaTime * smoothingFactor);

            //�������� ī�޶� �̵�
            cameraTransform.position = transform.position + (cameraTargetTransform.position - transform.position).normalized * currentDistance;

        }

        
        float GetCameraDistance()
        {
            RaycastHit _hit;

            //ī�޶� Ÿ��Ʈ�������� �����̴°� �ƴϰ� ī�޶� �����̹Ƿ� �׻� �����Ÿ��� ��ȯ
            Vector3 _castDirection = cameraTargetTransform.position - transform.position;

                if (Physics.Raycast(new Ray(transform.position, _castDirection), out _hit, _castDirection.magnitude + minimumDistanceFromObstacles, layerMask, QueryTriggerInteraction.Ignore))
                {
                    //�������ɰ�� ī�޶� ��ġ���̻������Ƿ� ����.
                    if (_hit.distance - minimumDistanceFromObstacles < 0f)
                        return _hit.distance;
                    else
                        return _hit.distance - minimumDistanceFromObstacles;
                }

            return _castDirection.magnitude;
        }

    }
}

