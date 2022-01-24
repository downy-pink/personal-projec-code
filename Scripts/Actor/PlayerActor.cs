using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using input;
using StateStructure;
using Actions;
using SonVectorMath;
using Helper;
using Core;
using DG.Tweening;

namespace Actors
{
    //�÷��̾ �����Ҽ��ֵ��� �ϴ� ��� ���(���� ������)
    public class PlayerActor : Actor, IKnockbackable, IDotDamageable
    {
        #region �ݶ��̴� ����
        [Header("�ݶ��̴� ����")]
        CapsuleCollider col;
        [SerializeField] float colliderHeight = 2f;
        [SerializeField] float colliderThickness = 1f;
        [SerializeField] Vector3 colliderOffset = Vector3.zero;
        #endregion

     
        #region �����Ӱ��� ����
        [Header("�����Ӱ��� ����")]
        Vector2 moveInputValue;
        [SerializeField] float gravity;
        [HideInInspector] public bool isjump;
        [HideInInspector] public bool isAttack;
        [HideInInspector] public bool isRightMouseClick;
        [HideInInspector] public bool isEvasionKey;
        internal Vector3 momentum; //����,�߷�, ������ ���
        Vector3 verticalM;
        Vector3 horizontalM;
        internal Vector3 nonMomentVelocity; //����, ���� �ӵ�(����� ��)
       
        #endregion

        #region ���� ����
        [Header("���� ����")]
        internal RaySensor sensor;
        [SerializeField] [Range(0f,1f)] float stepHeightRatio = 0.25f; //��� ���� �������� ���� ����� ������ �ִ�.
        float baseSensorRange = 0f; //�⺻ ���� ����
        bool isSensorExtendedRange = false; //������ Ȯ���� �ʿ��� �� ���
        public bool isSensorDebugMode;
        Vector3 currentGroundAdjustmentVel;
        #endregion

        [SerializeField] InputBroadCaster inputBC;
        public TransformValueSO playerTValue;
        [SerializeField] PlayerStateInfoSO playerInfoSO;
        [HideInInspector] Rigidbody rig;
        public UnityAction interactionEvents = delegate { };
        [SerializeField] BoxCollider animationBodyCol; //�����Ӱ��� �ִϸ��̼ǿ� ���Ǵ� �ݶ��̴�
        [SerializeField] GameEvents gameEventsSO;

        [SerializeField] GameObject windMoveEffect;
        [SerializeField] GameObject evasionEffect;
        [SerializeField] GameObject swordRTrail;
        [SerializeField] GameObject swordLTrail;
        [SerializeField] GameObject swordSkillAura;
        [SerializeField] GameObject swordStandardTrail;
        [SerializeField] GameObject potionEatEffect;

        bool isEvasion;
        bool isQInput;
        bool isEInput;
        bool isRInput;
        private void OnEnable()
        {
            inputBC.moveEvent += OnMove;
            inputBC.jumpEvent += OnJump;
            inputBC.attackEvent += OnAttack;
            inputBC.mouseRightClickEvent += OnRightMouseKeyDown;
            inputBC.evasionEvent += OnEvasionKey;
            inputBC.interactionEvent += OnInteraction;
            inputBC.qInputEvent += OnQSkill;
            inputBC.eInputEvent += OnESkill;
            inputBC.rInputEvent += OnRSkill;

            gameEventsSO.conversationEvent += OnConversation;
            gameEventsSO.conversationExitEvent += OnConversationExit;
        }

        private void OnDisable()
        {
            inputBC.moveEvent -= OnMove;
            inputBC.jumpEvent -= OnJump;
            inputBC.attackEvent -= OnAttack;
            inputBC.mouseRightClickEvent -= OnRightMouseKeyDown;
            inputBC.evasionEvent -= OnEvasionKey;
            inputBC.interactionEvent -= OnInteraction;
            inputBC.qInputEvent -= OnQSkill;
            inputBC.eInputEvent -= OnESkill;
            inputBC.rInputEvent -= OnRSkill;

            gameEventsSO.conversationEvent -= OnConversation;
            gameEventsSO.conversationExitEvent -= OnConversationExit;
        }

        void OnConversation()
        {
            inputBC.attackEvent -= OnAttack;
        }

        void OnConversationExit()
        {
            inputBC.attackEvent += OnAttack;
        }



        private void Awake()
        {
            Setup();
            LayerMask _layer = new LayerMask();
            //����Ʈ ������ ���̾ �����ϰڴ�.
            _layer = 1 << LayerMask.NameToLayer("Default");
            sensor = new RaySensor(transform, 1f, _layer, col);
            RecalculateColliderDimensions();
            RecalibrateSensor();
            sensor.debugMode = isSensorDebugMode;
            playerTValue.Variable = gameObject.transform;
            playerInfoSO.playerStateM = GetComponent<StateMachine>();

            DeactiveWindMoveEffect();
            DeactiveSwordLTrail();
            DeactiveSwordRTrail();
            DeactiveSwordSkillAura();
            DeactiveSwordStandardTrail();
            DeactiveEvasionEffect();
            DeactivepotionEatEffect();
        }

        private void OnValidate()
        {
            if (this.gameObject.activeInHierarchy)
                RecalculateColliderDimensions();
        }

        private void FixedUpdate()
        {
            Check();
            //momentum -= transform.up * 20 * Time.fixedDeltaTime;
            SetVelocity(momentum + nonMomentVelocity);

        }

        #region ��� ����
        //����ӵ��� �ڿ��������� ���� ��� ����, ������ ��ȯ������ ����
        internal void OnGroundContactLost()
        {
            Vector3 _velocity = nonMomentVelocity;
            if (nonMomentVelocity.sqrMagnitude >= 0f && momentum.sqrMagnitude > 0f)
            {
                //������� �ӵ��� ���� ���и��� ������.
                Vector3 _projectedMomentum = Vector3.Project(momentum, nonMomentVelocity.normalized);
                //�ӵ��� ���������� �ִ��� Ȯ�������� �ʿ�
                float _dot = VectorMath.GetVectorProjectionfloat(_projectedMomentum.normalized, nonMomentVelocity.normalized);

                //����ҿ� ����ִ� �ӵ��Ͱ��� ������ �ӵ����� ũ�� ����Ұ� �ӵ��� �������� ������ �����̶� �ִٸ�
                if (_projectedMomentum.sqrMagnitude >= _velocity.sqrMagnitude && _dot > 0f)
                    _velocity = Vector3.zero;
                else if (_dot > 0f)
                    _velocity -= _projectedMomentum;
            }
   
            //Add movement velocity to momentum;
            momentum += _velocity;
        }


        public Vector3 GetVerticalMomentum()
        {
            verticalM = Vector3.zero;
            if(momentum != Vector3.zero)
            verticalM = VectorMath.VectorProjection(momentum, transform.up);
            return verticalM;
        }

        public Vector3 GetHorizontalMomentum()
        {
            horizontalM = Vector3.zero;
            if (momentum != Vector3.zero)
            {
                verticalM = VectorMath.VectorProjection(momentum, transform.up);
                horizontalM = momentum - verticalM;
            }
            return horizontalM;
        }


        public void ForwardAddMomentum()
        {
            AddMomentum(modelRoot.transform.forward * 6);
        }

        public void ForwardAddMomentum(float _addForwardMomentum)
        {
            AddMomentum(modelRoot.transform.forward * _addForwardMomentum);
        }

        public void AddMomentum(Vector3 _momentum)
        {
            momentum += _momentum;
        }
        #endregion

        #region ����

        public void Setup()
        {
            col = GetComponent<CapsuleCollider>();

            if (col == null)
            {
                transform.gameObject.AddComponent<CapsuleCollider>();
                col = GetComponent<CapsuleCollider>();
            }

            rig = GetComponent<Rigidbody>();

            if (rig == null)
            {
                transform.gameObject.AddComponent<Rigidbody>();
                rig = GetComponent<Rigidbody>();
            }

            rig.freezeRotation = true;
            rig.useGravity = false;
        }

        public void RecalculateColliderDimensions()
        {
            if (col == null)
            {
                Setup();

                if (col == null)
                {
                    Debug.LogWarning("There is no collider attached to " + this.gameObject.name + "!");
                    return;
                }
            }

            col.height = colliderHeight;
            col.center = colliderOffset * colliderHeight;
            col.radius = colliderThickness / 2f;

            col.center = col.center + new Vector3(0f, stepHeightRatio * col.height / 2f, 0f);
            col.height *= (1f - stepHeightRatio);

            if (col.height / 2f < col.radius)
                col.radius = col.height / 2f;

            if (sensor != null)
                RecalibrateSensor();
        }

        void RecalibrateSensor()
        {
            float _length = 0f;
            float _safetyDistanceFactor = 0.001f;

            _length += (colliderHeight * (1f - stepHeightRatio)) * 0.5f;
            _length += colliderHeight * stepHeightRatio;
            baseSensorRange = _length * (1f + _safetyDistanceFactor) * transform.localScale.x;
            sensor.castLength = _length * transform.localScale.x;
            sensor.SetOrigin(col.bounds.center);
        }

        public void Check()
        {
            currentGroundAdjustmentVel = Vector3.zero;

            if (isSensorExtendedRange)
                sensor.castLength = baseSensorRange + (colliderHeight * transform.localScale.x) * stepHeightRatio;
            else
                sensor.castLength = baseSensorRange;

            sensor.GetRay();

            if (!sensor.ishit)
                return;

            float _distance = sensor.GetDistance();
      
            //������ Ȯ���� ���� �� y���� �����ֹǷ� Ȯ���� ��� ��.
            float _upperLimit = ((colliderHeight * transform.localScale.x) * (1f - stepHeightRatio)) * 0.5f;
            float _middle = _upperLimit + (colliderHeight * transform.localScale.x) * stepHeightRatio;
            float _distanceToGo = _middle - _distance;

            //FixedUpdate���� ���� ��ų ���̱� ������ Time.fixedDeltaTime���� ������
            currentGroundAdjustmentVel = transform.up * (_distanceToGo / Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            sensor.DrawRay();
        }

        public void IsSensorExtendedRange(bool _isSensorExtendedRange)
        {
            isSensorExtendedRange = _isSensorExtendedRange;
        }

        #endregion

        public void SetEvasion(bool _isEvasion)
        {
            isEvasion = _isEvasion;
        }

        public override void Damage(DamageMessage _damageM)
        {
            if (!isBlock && !isEvasion)
            {
                stats.DeacreaseHP(_damageM.damage);
                hitEvent();
                TryCameraShake(0, 0.15f, 2.5f, 60);
                bloodEffect.Onhit(transform, _damageM.hitmessage);
                enemyT = _damageM.enemyT;
            }
            Debug.Log(stats.GetHP());

        }

        float currentDotDamge;
        public void Damage(DotDamageMessage _dotDamageM)
        {
            currentDotDamge += Time.deltaTime;

            if (currentDotDamge >= 1)
            {
                currentDotDamge = 0;
                stats.DeacreaseHP(_dotDamageM.damage);
                bloodEffect.Onhit(transform, _dotDamageM.hitmessage);
                Debug.Log(stats.GetHP());
            }
        }
        
        public override void Block(DamageMessage _damageM)
        {
            if (isBlock)
            {
                isBlock = IsinAngle(_damageM.enemyT);
            }

            if (_damageM.enemyT != null && isBlock)
            {
                Vector3 _v = transform.position - _damageM.enemyT.position;
                _v = VectorMath.RemoveVector(_v, Vector3.up);
                Sequence _s = DOTween.Sequence();
                _s
                .Append(transform.DOMove(transform.position + _v.normalized * 2f, 0.2f).SetEase(blockEase));
                StartCoroutine(DoBlockEffectCoroutine());
                isBlockhit = true;
                TryCameraShake(0, 0.15f, 1.5f, 60);
            }
        }



        public void Knockback(KnockbackMessage _knockbackM)
        {
            Vector3 _knockbacForceDir = (transform.position - _knockbackM.enemyT.position).normalized;
            AddMomentum(_knockbacForceDir * _knockbackM.knockback);
        }

        public Vector3 SetnonMomentVelocity(float _speed, Transform _camera)
        {
            Vector3 _movementVelocity = Vector3.zero;
            _movementVelocity += Vector3.ProjectOnPlane(_camera.right, transform.up).normalized * GetmoveInputValue().x;
            _movementVelocity += Vector3.ProjectOnPlane(_camera.forward, transform.up).normalized * GetmoveInputValue().y;
            if (_movementVelocity.magnitude > 1f)
                _movementVelocity.Normalize();

            _movementVelocity *= _speed;
            return _movementVelocity;
        }

        public void SetVelocity(Vector3 _velocity)
        {
            rig.velocity = _velocity + currentGroundAdjustmentVel;
        }
        public Vector3 GetGroundNormal()
        {
            return sensor.GetGroundNormal();
        }

        public Rigidbody GetRigid()
        {
            return rig;
        }

        public Vector2 GetmoveInputValue()
        {
            return moveInputValue;
        }

        void OnEvasionKey(bool _isEvasionKey)
        {
            isEvasionKey = _isEvasionKey;
        }

        void OnRightMouseKeyDown(bool _isRightMouseClick)
        {
            isRightMouseClick = _isRightMouseClick;
        }

        void OnMove(Vector2 _moveinputValue)
        {
            moveInputValue = _moveinputValue;
        }

        void OnJump(bool _isjump)
        {
            isjump = _isjump;
        }

        void OnAttack(bool _isAttack)
        {
            isAttack = _isAttack;
        }


        void OnInteraction()
        {
            if (interactionEvents != null)
                interactionEvents.Invoke();
        }

        void OnQSkill(bool _isQInput) => isQInput = _isQInput;

        void OnESkill(bool _isEInput) => isEInput = _isEInput;

        void OnRSkill(bool _isRInput) => isRInput = _isRInput;

        public bool GetIsQInput() => isQInput;

        public bool GetIsEInput() => isEInput;

        public bool GetIsRInput() => isRInput;

        public void ActiveWindMoveEffect() => windMoveEffect.active = true;

        public void DeactiveWindMoveEffect() => windMoveEffect.active = false;

        public void ActiveSwordRTrail() => swordRTrail.active = true;

        public void DeactiveSwordRTrail() => swordRTrail.active = false;

        public void ActiveSwordLTrail() => swordLTrail.active = true;

        public void DeactiveSwordLTrail() => swordLTrail.active = false;

        public void ActiveSwordSkillAura() => swordSkillAura.active = true;

        public void DeactiveSwordSkillAura() => swordSkillAura.active = false;

        public void ActiveSwordStandardTrail() => swordStandardTrail.active = true;

        public void DeactiveSwordStandardTrail() => swordStandardTrail.active = false;

        public void ActiveEvasionEffect() => evasionEffect.active = true;

        public void DeactiveEvasionEffect() => evasionEffect.active = false;

        public void ActivepotionEatEffect() => potionEatEffect.active = true;

        public void DeactivepotionEatEffect() => potionEatEffect.active = false;


        public BoxCollider GetAnimationBodyCol() => animationBodyCol;

        public void TryCameraShake(float _dalay, float _duration, float _strength = 90, int _vibrato = 10)
        {
            StartCoroutine(DoCameraShake(_dalay, _duration, _strength, _vibrato));
        }

        IEnumerator DoCameraShake(float _dalay, float _duration, float _strength, int _vibrato)
        {
            yield return new WaitForSeconds(_dalay);
            Camera.main.transform.DOShakeRotation(_duration, _strength, _vibrato);
        }



    }
}

