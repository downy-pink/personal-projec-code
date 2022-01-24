using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DistanceAttack : Attack
    {
        [SerializeField] float speed;
        [SerializeField] LayerMask targetLayer;

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        //public override void OnCollisionEnter(Collision collision)
        //{
        //    IDamageable _idamage = collision.collider.GetComponent<IDamageable>();

        //    if (_idamage != null && targetLayer == (targetLayer | (1 << collision.collider.gameObject.layer))
        //        && targetLayer == (targetLayer | (1 << collision.collider.gameObject.layer)))
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
        //        ColliderDisabled();
        //        Destroy(gameObject, 3);
        //    }

        //}

        public override void OnTriggerEnter(Collider other)
        {
            IDamageable _idamage = other.GetComponent<IDamageable>();

            if (_idamage != null && targetLayer == (targetLayer | (1 << other.gameObject.layer))
                && targetLayer == (targetLayer | (1 << other.gameObject.layer)))
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
                ColliderDisabled();
                Destroy(gameObject, 3);
            }

        }
    }
}

