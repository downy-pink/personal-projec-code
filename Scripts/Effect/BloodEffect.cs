using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Actors;

namespace Effects
{
    public class BloodEffect : MonoBehaviour
    {
        public bool InfiniteDecal;
        public Light DirLight;
        public bool isVR = true;
        public GameObject BloodAttach;
        public GameObject[] BloodFX;

        Transform GetNearestObject(Transform hit, Vector3 hitPos)
        {
            var closestPos = 100f;
            Transform closestBone = null;
            var childs = hit.GetComponentsInChildren<Transform>();

            foreach (var child in childs)
            {
                var dist = Vector3.Distance(child.position, hitPos);
                if (dist < closestPos)
                {
                    closestPos = dist;
                    closestBone = child;
                }
            }

            var distRoot = Vector3.Distance(hit.position, hitPos);
            if (distRoot < closestPos)
            {
                closestPos = distRoot;
                closestBone = hit;
            }
            return closestBone;
        }

        public Vector3 direction;
        int effectIdx;


        public void Onhit(Transform _myT, hitMessage _hitmessage)
        {


            // var randRotation = new Vector3(0, Random.value * 360f, 0);
            // var dir = CalculateAngle(Vector3.forward, hit.normal);
            float angle = Mathf.Atan2(_hitmessage.hitnormal.x, _hitmessage.hitnormal.z) * Mathf.Rad2Deg + 180;

            //var effectIdx = Random.Range(0, BloodFX.Length);
            if (effectIdx == BloodFX.Length) effectIdx = 0;

            var instance = Instantiate(BloodFX[effectIdx], _hitmessage.hitPoint, Quaternion.Euler(0, angle + 90, 0));
            instance.GetComponent<BFX_BloodSettings>().GroundHeight = transform.position.y;
            Destroy(instance, 5f);
            effectIdx++;

            var settings = instance.GetComponent<BFX_BloodSettings>();
            //settings.FreezeDecalDisappearance = InfiniteDecal;
            settings.LightIntensityMultiplier = DirLight.intensity;

            var nearestBone = GetNearestObject(_myT.root, _hitmessage.hitPoint);
            if (nearestBone != null)
            {
                var attachBloodInstance = Instantiate(BloodAttach);
                Destroy(attachBloodInstance, 15f);
                var bloodT = attachBloodInstance.transform;
                bloodT.position = _hitmessage.hitPoint;
                bloodT.localRotation = Quaternion.identity;
                bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
                bloodT.LookAt(_hitmessage.hitPoint + _hitmessage.hitnormal, direction);
                bloodT.Rotate(90, 0, 0);
                bloodT.transform.parent = nearestBone;
                //Destroy(attachBloodInstance, 20);
            }

            if (!InfiniteDecal) Destroy(instance, 20);


            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit))
            //{
            //    // var randRotation = new Vector3(0, Random.value * 360f, 0);
            //    // var dir = CalculateAngle(Vector3.forward, hit.normal);
            //    float angle = Mathf.Atan2(hit.normal.x, hit.normal.z) * Mathf.Rad2Deg + 180;

            //    //var effectIdx = Random.Range(0, BloodFX.Length);
            //    if (effectIdx == BloodFX.Length) effectIdx = 0;

            //    var instance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.Euler(0, angle + 90, 0));
            //    effectIdx++;

            //    var settings = instance.GetComponent<BFX_BloodSettings>();
            //    //settings.FreezeDecalDisappearance = InfiniteDecal;
            //    settings.LightIntensityMultiplier = DirLight.intensity;

            //    var nearestBone = GetNearestObject(hit.transform.root, hit.point);
            //    if (nearestBone != null)
            //    {
            //        var attachBloodInstance = Instantiate(BloodAttach);
            //        var bloodT = attachBloodInstance.transform;
            //        bloodT.position = hit.point;
            //        bloodT.localRotation = Quaternion.identity;
            //        bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            //        bloodT.LookAt(hit.point + hit.normal, direction);
            //        bloodT.Rotate(90, 0, 0);
            //        bloodT.transform.parent = nearestBone;
            //        //Destroy(attachBloodInstance, 20);
            //    }

            //    // if (!InfiniteDecal) Destroy(instance, 20);

            //}
        }


        public float CalculateAngle(Vector3 from, Vector3 to)
        {

            return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;

        }

    }

}
