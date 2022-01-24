using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Core;
using Effects;
using StateStructure;

namespace Actors
{

    public class MonsterActor : Actor, IDotDamageable, IParryiable
    {
        //순회 지점
        [SerializeField] internal Transform[] patrolT;
        [SerializeField] string[] attackName;
        int attackNum;

        public UnityAction EnabledCombat = delegate { };
        public UnityAction DisabledCombat = delegate { };
        //전투에 돌입했는가?
        [SerializeField] bool isCombat;

        [SerializeField] GameObject comboAttackEffect;
        [SerializeField] GameObject attackEffect;

        public bool isAttack;

        bool isParry;

        public override void Start()
        {
            base.Start();
            stats = GetComponent<Stats>();
            bloodEffect = GetComponent<BloodEffect>();

            DeactiveComboAttackEffect();
            DeactiveAttackEffect();
        }

        

        // Start is called before the first frame update
        public string AttackStringChange()
        {
            if (attackNum >= attackName.Length)
                attackNum = 0;
            if (attackName != null)
                return attackName[attackNum++];
            return null; //null일 경우
        }

        public string GetAttackString()
        {
            if (attackNum == 0)
                return attackName[0];
            return attackName[attackNum - 1]; //후위 연산자를 사용하므로
        }

        //플레이어를 발견한적이 한번이라도 있다면 활성화
        public void SetIsCombat(bool _isCombat)
        {
            isCombat = _isCombat;
        }

        public bool GetIsCombat()
        {
            return isCombat;
        }

        //ai가 이동할 수 있는 가장 가까운 위치로 이동
        public bool movePoint(Vector3 _position, out NavMeshHit _hit)
        {
            return NavMesh.SamplePosition(_position, out _hit, 1.0f, NavMesh.AllAreas);
        }

        public override void Damage(DamageMessage _damageM)
        {
            if (!isBlock && !isParry)
            {
                if(stats != null)
                stats.DeacreaseHP(_damageM.damage);
                hitEvent();
                if(bloodEffect != null)
                bloodEffect.Onhit(transform, _damageM.hitmessage);
                enemyT = _damageM.enemyT;
            }
            if (isParry)
                isParry = false;
        }

        float currentDotDamge;
        public void Damage(DotDamageMessage _dotDamageM)
        {
            currentDotDamge += Time.deltaTime;

            if (currentDotDamge >= 1)
            {
                currentDotDamge = 0;
                stats.DeacreaseHP(_dotDamageM.damage);
                hitEvent();
                bloodEffect.Onhit(transform, _dotDamageM.hitmessage);
                Debug.Log(stats.GetHP());
            }
        }

        public void ActiveComboAttackEffect()
        {
            if (comboAttackEffect != null)
                comboAttackEffect.active = true;
        }

        public void DeactiveComboAttackEffect()
        {
            if(comboAttackEffect != null)
            comboAttackEffect.active = false;
        }

        public void ActiveAttackEffect()
        {
            if (attackEffect != null)
                attackEffect.active = true;
        }

        public void DeactiveAttackEffect()
        {
            if (attackEffect != null)
                attackEffect.active = false;
        }

        public void Parrying()
        {
            isParry = true;
            hitEvent();
        }
    }

}
