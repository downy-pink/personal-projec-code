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
    //플레이어가 동작할수있도록 하는 기능 담당(상태 제외한)
    public class PlayerActor : Actor, IKnockbackable, IDotDamageable
    {
        #region 콜라이더 관련
        [Header("콜라이더 관련")]
        CapsuleCollider col;
        [SerializeField] float colliderHeight = 2f;
        [SerializeField] float colliderThickness = 1f;
        [SerializeField] Vector3 colliderOffset = Vector3.zero;
        #endregion

     
        #region 움직임관련 변수
        [Header("움직임관련 변수")]
        Vector2 moveInputValue;
        [SerializeField] float gravity;
        [HideInInspector] public bool isjump;
        [HideInInspector] public bool isAttack;
        [HideInInspector] public bool isRightMouseClick;
        [HideInInspector] public bool isEvasionKey;
        internal Vector3 momentum; //마찰,중력, 점프의 운동량
        Vector3 verticalM;
        Vector3 horizontalM;
        internal Vector3 nonMomentVelocity; //수평, 수직 속도(운동량을 뺀)
       
        #endregion

        #region 센서 관련
        [Header("센서 관련")]
        internal RaySensor sensor;
        [SerializeField] [Range(0f,1f)] float stepHeightRatio = 0.25f; //계단 높이 높을수록 높은 계단을 오를수 있다.
        float baseSensorRange = 0f; //기본 센서 범위
        bool isSensorExtendedRange = false; //센서의 확장이 필요할 때 사용
        public bool isSensorDebugMode;
        Vector3 currentGroundAdjustmentVel;
        #endregion

        [SerializeField] InputBroadCaster inputBC;
        public TransformValueSO playerTValue;
        [SerializeField] PlayerStateInfoSO playerInfoSO;
        [HideInInspector] Rigidbody rig;
        public UnityAction interactionEvents = delegate { };
        [SerializeField] BoxCollider animationBodyCol; //움직임관련 애니메이션에 사용되는 콜라이더
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
            //디폴트 제외한 레이어를 무시하겠다.
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

        #region 운동량 관련
        //수평속도의 자연스러움을 위해 운동량 조절, 공중의 전환점에서 실행
        internal void OnGroundContactLost()
        {
            Vector3 _velocity = nonMomentVelocity;
            if (nonMomentVelocity.sqrMagnitude >= 0f && momentum.sqrMagnitude > 0f)
            {
                //운동량에서 속도와 같은 성분만을 빼낸다.
                Vector3 _projectedMomentum = Vector3.Project(momentum, nonMomentVelocity.normalized);
                //속도와 같은성분이 있는지 확인을위해 필요
                float _dot = VectorMath.GetVectorProjectionfloat(_projectedMomentum.normalized, nonMomentVelocity.normalized);

                //모멘텀에 들어있는 속도와같은 성분이 속도보다 크고 모멘텀과 속도가 같은방향 성분이 조금이라도 있다면
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

        #region 센서

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
      
            //레이의 확장이 있을 때 y값을 더해주므로 확장을 고려 함.
            float _upperLimit = ((colliderHeight * transform.localScale.x) * (1f - stepHeightRatio)) * 0.5f;
            float _middle = _upperLimit + (colliderHeight * transform.localScale.x) * stepHeightRatio;
            float _distanceToGo = _middle - _distance;

            //FixedUpdate에서 적용 시킬 것이기 때문에 Time.fixedDeltaTime으로 나누기
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

