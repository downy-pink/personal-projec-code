using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStructure;
using Actions;
using UnityEngine.Events;
using DG.Tweening;
using SonVectorMath;
using Core;
using Effects;

namespace Actors
{
    //���±�踦 �����Ͽ� �����°�, ���°����� Ŀ�ø��� ���⼭ �� �Ͼ��.
    //���±��� prviate���� �����Ͽ� �ܺ� Ŀ�ø��� �ִ��� ���´�.
    public class Actor : MonoBehaviour, IDamageable
    {
        public Transform modelRoot; //���� �����̴� ��
        [SerializeField] protected Ease blockEase;
        public UnityAction hitEvent;

        protected Stats stats;

        protected bool isBlock;

        protected Transform enemyT;

        protected BloodEffect bloodEffect;

        [SerializeField] GameObject blockHitEffect;

        [HideInInspector] public bool isEvasion;

        StateMachine sm;

        protected bool isBlockhit;
        public virtual void Start()
        {
            stats = GetComponent<Stats>();
            bloodEffect = GetComponent<BloodEffect>();
            blockHitEffect.active = false;
            sm = GetComponent<StateMachine>();
        }


        public void SetBlock(bool _isBlock)
        {
            isBlock = _isBlock;
        }


        //������ �̻����� ���̰����� ��������
        public bool IsinAngle(Transform _enemyt)
        {
            Vector3 efm = _enemyt.position - modelRoot.position;
            efm = VectorMath.RemoveVector(efm, Vector3.up); //y������ ����.
            return Vector3.Angle(modelRoot.forward, efm.normalized) <= 75;
        }

        //��밡 ȣ��
        public virtual void Block(DamageMessage _damageM)
        {
            if(isBlock)
            {
                isBlock = IsinAngle(_damageM.enemyT);
            }

            if (_damageM.enemyT != null && isBlock && !isEvasion)
            {
                Vector3 _v = transform.position - _damageM.enemyT.position;
                _v = VectorMath.RemoveVector(_v, Vector3.up);
                Sequence _s = DOTween.Sequence();
                _s
                .Append(transform.DOMove(transform.position + _v.normalized * 2f, 0.2f).SetEase(blockEase));
                isBlockhit = true;
                StartCoroutine(DoBlockEffectCoroutine());
            }
        }

        public void SetIsBlockhit(bool _isBlockhit) => isBlockhit = _isBlockhit;
        public bool GetIsBlockhit() => isBlockhit;

        public Transform GetEnemyT()
        {
            return enemyT;
        }

        public virtual void Hit()
        {

        }

        public virtual void Damage(DamageMessage _damageM)
        {
            if (!isBlock)
            {
                stats.DeacreaseHP(_damageM.damage);
                hitEvent();
                bloodEffect.Onhit(transform, _damageM.hitmessage);
                enemyT = _damageM.enemyT;
            }
            Debug.Log(stats.GetHP());

        }

        protected IEnumerator DoBlockEffectCoroutine()
        {
            blockHitEffect.active = true;
            yield return new WaitForSeconds(0.4f);
            blockHitEffect.active = false;
        }

      
    }


}



