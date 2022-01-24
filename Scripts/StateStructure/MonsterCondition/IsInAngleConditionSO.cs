using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;
using SonVectorMath;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsInAngleConditionSO", menuName = "State Machines/Conditions/IsInAngleCondition")]
    //���Ϳ� ������Ʈ �� ���ֺ��� ���� ���̸� ���Ͽ� ���̰� ��� ������ true
    public class IsInAngleConditionSO : StateConditionSO<IsInAngleCondition>
    {
        //���� ��� ����
        public float inAngle => _inAngle;
        public TransformValueSO tValue => _tValue;
        public bool isCombatNotAffected => _isCombatNotAffected;

        [SerializeField] float _inAngle;
        [SerializeField] TransformValueSO _tValue;
        [SerializeField] bool _isCombatNotAffected;
        protected override Condition CreateCondition() => new IsInAngleCondition();
    }

    public class IsInAngleCondition : Condition
    {
        private IsInAngleConditionSO originSO => (IsInAngleConditionSO)base.OriginSO;

        MonsterActor monster;
        bool isCombat;
        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();
            monster.EnabledCombat += EnabledCombat;
            monster.DisabledCombat += DisableCombat;
        }

        void EnabledCombat()
        {
            isCombat = true;
        }

        void DisableCombat()
        {
            isCombat = false;
        }

        protected override bool Statement()
        {
            if (isCombat && !originSO.isCombatNotAffected)
                return true;

            if (Vector3.Distance(originSO.tValue.Variable.transform.position, monster.transform.position) <= 3.5f)
                return true;
            Vector3 mfp = originSO.tValue.Variable.transform.position - monster.transform.position;
            mfp = VectorMath.RemoveVector(mfp, Vector3.up); //y������ ����.
            return Vector3.Angle(monster.transform.forward, mfp.normalized) <= originSO.inAngle;
        }

    }
}

