using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IDotDamageable
    {
        void Damage(DotDamageMessage _dotDamageM);
    }

    public class DotDamageMessage
    {
        public float damage;
        public hitMessage hitmessage = new hitMessage();
    }

    public class DotAttack : MonoBehaviour
    {
        [SerializeField] Collider attackPointCol;
        [SerializeField] LayerMask targetLayer; //공격판정 당하는 레이어
        [SerializeField] float dotDamage = 2;

        private void OnTriggerStay(Collider other)
        {
            IDotDamageable iDotDamageable = other.GetComponent<IDotDamageable>();
            
            if (iDotDamageable != null && targetLayer == (targetLayer | (1 << other.gameObject.layer)))
            {

                DotDamageMessage dotDamageM = new DotDamageMessage();
                dotDamageM.hitmessage.hitPoint = other.ClosestPoint(attackPointCol.transform.position);
                dotDamageM.hitmessage.hitnormal = attackPointCol.ClosestPoint(other.transform.position) - dotDamageM.hitmessage.hitPoint;

                dotDamageM.damage = dotDamage;
                iDotDamageable.Damage(dotDamageM);
            }
        }
    }
}

