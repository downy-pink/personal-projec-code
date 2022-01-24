using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Effects;
using Actors;

namespace Core
{
    public interface IDamageable
    {
        void Block(DamageMessage _damageM);
        void Damage(DamageMessage _damageM);
    }

    public class DamageMessage
    {
        public float damage;
        public float knockback;
        public Transform enemyT;
        public hitMessage hitmessage = new hitMessage();

    }

    public interface IKnockbackable
    {
        void Knockback(KnockbackMessage _knockbackM);
    }

    public class KnockbackMessage
    {
        public float knockback;
        public Transform enemyT;
    }

    public interface IParryiable
    {
        void Parrying();
    }


    public class hitMessage
    {
        public Vector3 hitnormal;
        public Vector3 hitPoint;
    }

    public class Attack : MonoBehaviour
    {
        [SerializeField] protected float damage;
        public GameObject obj;
        Transform enemyT;
        [SerializeField] bool isDective;
        public UnityAction colDeactive = delegate { };
        protected Collider attackPointCol;
        PlayerActor player;

        [SerializeField] LayerMask targetAttackLayer; //공격판정 당하는 레이어

        [SerializeField] GameObject parryHitEffect;
        private void Start()
        {
            attackPointCol = obj.GetComponent<Collider>();
            if (!isDective)
            obj.SetActive(false);
            player = GetComponent<PlayerActor>();

        }

        public void ColEnabled()
        {
            obj.SetActive(true);
        }

        public void ColDisabled()
        {
            obj.SetActive(false);
        }

        public bool GetobjActive()
        {
            return obj.active;
        }

        public void ColliderDisabled()
        {
            attackPointCol.enabled = false;
        }

        //public virtual void OnCollisionEnter(Collision collision)
        //{
        //    IDamageable _idamage = collision.collider.GetComponent<IDamageable>();

        //    if (_idamage != null && targetAttackLayer == (targetAttackLayer | (1 << collision.collider.gameObject.layer)))
        //    {
        //        Debug.Log(collision.collider.name);
        //        DamageMessage _damageM = new DamageMessage();
        //        _damageM.enemyT = transform;
        //        _damageM.damage = damage;
        //        _damageM.hitmessage.hitPoint = collision.collider.ClosestPoint(attackPointCol.transform.position);
        //        _damageM.hitmessage.hitnormal = attackPointCol.ClosestPoint(collision.collider.transform.position) - _damageM.hitmessage.hitPoint;
        //        _idamage.Block(_damageM);
        //        _idamage.Damage(_damageM);
        //        if (player != null)
        //            player.TryCameraShake(0, 0.15f, 1.5f, 60);
        //        if (colDeactive != null)
        //            colDeactive.Invoke();
        //        ColDisabled();
        //    }
        //}

        //public virtual void OnCollisionEnter(Collision collision)
        //{
        //    IDamageable _idamage = collision.other.GetComponent<IDamageable>();
        //    IParryiable _iparry = collision.other.GetComponent<IParryiable>();
        //    MonsterActor monster = collision.other.GetComponent<MonsterActor>();
        //    if (_iparry != null && monster != null && monster.isAttack && targetAttackLayer == (targetAttackLayer | (1 << collision.other.gameObject.layer)))
        //    {
        //        GameObject _parryEffect = Instantiate(parryHitEffect, collision.collider.ClosestPoint(attackPointCol.transform.position)
        //            - transform.forward * 0.45f, Quaternion.identity);
        //        Destroy(_parryEffect, 0.3f);

        //        player.AddMomentum(-transform.up);
        //        player.TryCameraShake(0, 0.15f, 1.5f, 60);
        //        _iparry.Parrying();
        //        player.hitEvent();
        //        ColDisabled();
        //    }

        //    else if (_idamage != null && targetAttackLayer == (targetAttackLayer | (1 << collision.collider.gameObject.layer)))
        //    {
        //        //Debug.Log(other.name);
        //        DamageMessage _damageM = new DamageMessage();
        //        _damageM.enemyT = transform;
        //        _damageM.damage = damage;
        //        _damageM.hitmessage.hitPoint = collision.collider.ClosestPoint(attackPointCol.transform.position);
        //        _damageM.hitmessage.hitnormal = attackPointCol.ClosestPoint(collision.collider.transform.position) - _damageM.hitmessage.hitPoint;
        //        _idamage.Block(_damageM);
        //        _idamage.Damage(_damageM);
        //        if (player != null)
        //            player.TryCameraShake(0, 0.15f, 1.5f, 60);
        //        if (colDeactive != null)
        //            colDeactive.Invoke();
        //        ColDisabled();
        //    }
        //}

        public virtual void OnTriggerEnter(Collider other)
        {
            IDamageable _idamage = other.GetComponent<IDamageable>();
            IParryiable _iparry = other.GetComponent<IParryiable>();
            //MonsterActor monster = other.GetComponent<MonsterActor>();
            //if (_iparry != null && monster != null && monster.isAttack && targetAttackLayer == (targetAttackLayer | (1 << other.gameObject.layer)))
            //{
            //    GameObject _parryEffect = Instantiate(parryHitEffect, other.ClosestPoint(attackPointCol.transform.position)
            //        - transform.forward * 0.45f, Quaternion.identity);
            //    Destroy(_parryEffect, 0.3f);

            //    player.AddMomentum(-transform.up);
            //    player.TryCameraShake(0, 0.15f, 1.5f, 60);
            //    _iparry.Parrying();
            //    player.hitEvent();
            //    ColDisabled();
            //}

            if (_idamage != null && targetAttackLayer == (targetAttackLayer | (1 << other.gameObject.layer)))
            {
                //Debug.Log(other.name);
                DamageMessage _damageM = new DamageMessage();
                _damageM.enemyT = transform;
                _damageM.damage = damage;
                _damageM.hitmessage.hitPoint = other.ClosestPoint(attackPointCol.transform.position);
                _damageM.hitmessage.hitnormal = attackPointCol.ClosestPoint(other.transform.position) - _damageM.hitmessage.hitPoint;
                _idamage.Block(_damageM);
                _idamage.Damage(_damageM);
                if (player != null)
                    player.TryCameraShake(0, 0.15f, 1.5f, 60);
                if (colDeactive != null)
                    colDeactive.Invoke();
                ColDisabled();
            }
        }

    }
}



