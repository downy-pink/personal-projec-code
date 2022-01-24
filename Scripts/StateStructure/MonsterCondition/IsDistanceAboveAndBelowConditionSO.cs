using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsDistanceAboveAndBelowConditionSO", menuName = "State Machines/Conditions/IsDistanceAboveAndBelowCondition")]
    public class IsDistanceAboveAndBelowConditionSO : StateConditionSO<IsHitCondition>
    {
        public TransformValueSO playerTSO => _playerTSO;
        public float distanceBelow => _distanceBelow;
        public float distanceAbove => _distanceAbove;

        [SerializeField] TransformValueSO _playerTSO;
        [SerializeField] float _distanceBelow;
        [SerializeField] float _distanceAbove;
        protected override Condition CreateCondition() => new IsDistanceAboveAndBelowCondition();
    }

    public class IsDistanceAboveAndBelowCondition : Condition
    {
        private IsDistanceAboveAndBelowConditionSO originSO => (IsDistanceAboveAndBelowConditionSO)base.OriginSO;
        MonsterActor monster;

        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();   
        }

        protected override bool Statement()
        {
                return originSO.distanceAbove <= Vector3.Distance(monster.transform.position, originSO.playerTSO.Variable.position)
                && Vector3.Distance(monster.transform.position, originSO.playerTSO.Variable.position) <= originSO.distanceBelow;
        }
    }
}

