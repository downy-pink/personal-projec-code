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
        //��ȸ ����
        [SerializeField] internal Transform[] patrolT;
        [SerializeField] string[] attackName;
        int attackNum;

        public UnityAction EnabledCombat = delegate { };
        public UnityAction DisabledCombat = delegate { };
        //������ �����ߴ°�?
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
            return null; //null�� ���
        }

        public string GetAttackString()
        {
            if (attackNum == 0)
                return attackName[0];
            return attackName[attackNum - 1]; //���� �����ڸ� ����ϹǷ�
        }

        //�÷��̾ �߰������� �ѹ��̶� �ִٸ� Ȱ��ȭ
        public void SetIsCombat(bool _isCombat)
        {
            isCombat = _isCombat;
        }

        public bool GetIsCombat()
        {
            return isCombat;
        }

        //ai�� �̵��� �� �ִ� ���� ����� ��ġ�� �̵�
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
