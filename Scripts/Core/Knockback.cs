using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Knockback : Attack
    {
        [SerializeField] float knockback;
        [SerializeField] LayerMask targetLayer;
        public void Setknockback(float _knockback) => knockback = _knockback;

        //public override void OnCollisionEnter(Collision collision)
        //{
        //    IDamageable _idamage = collision.collider.GetComponent<IDamageable>();
        //    IKnockbackable _iknockback = collision.collider.GetComponent<IKnockbackable>();

        //    if (_idamage != null && _iknockback != null && targetLayer == (targetLayer | (1 << collision.collider.gameObject.layer)))
        //    {
        //        DamageMessage _damageM = new DamageMessage();
        //        _damageM.enemyT = transform;
        //        _damageM.damage = damage;
        //        _damageM.hitmessage.hitPoint = collision.collider.ClosestPoint(attackPointCol.transform.position);
        //        _damageM.hitmessage.hitnormal = attackPointCol.ClosestPoint(collision.collider.transform.position) - _damageM.hitmessage.hitPoint;
        //        _idamage.Block(_damageM);
        //        _idamage.Damage(_damageM);
        //        if (colDeactive != null)
        //            colDeactive.Invoke();
        //    }

        //    if (_iknockback != null)
        //    {
        //        Debug.Log("dd");
        //        KnockbackMessage _knockbackM = new KnockbackMessage();
        //        _knockbackM.knockback = knockback;
        //        _knockbackM.enemyT = transform;
        //        _iknockback.Knockback(_knockbackM);
        //        ColDisabled();
        //    }
        //}

        public override void OnTriggerEnter(Collider other)
        {
            IDamageable _idamage = other.GetComponent<IDamageable>();
            IKnockbackable _iknockback = other.GetComponent<IKnockbackable>();

            if (_idamage != null && _iknockback != null && targetLayer == (targetLayer | (1 << other.gameObject.layer)))
            {
                DamageMessage _damageM = new DamageMessage();
                _damageM.enemyT = transform;
                _damageM.damage = damage;
                _damageM.hitmessage.hitPoint = other.ClosestPoint(attackPointCol.transform.position);
                _damageM.hitmessage.hitnormal = attackPointCol.ClosestPoint(other.transform.position) - _damageM.hitmessage.hitPoint;
                _idamage.Block(_damageM);
                _idamage.Damage(_damageM);
                if (colDeactive != null)
                    colDeactive.Invoke();
            }

            if (_iknockback != null)
            {
                Debug.Log("dd");
                KnockbackMessage _knockbackM = new KnockbackMessage();
                _knockbackM.knockback = knockback;
                _knockbackM.enemyT = transform;
                _iknockback.Knockback(_knockbackM);
                ColDisabled();
            }
        }
    }

}
