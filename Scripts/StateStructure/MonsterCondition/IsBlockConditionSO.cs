using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsBlockConditionSO", menuName = "State Machines/Conditions/IsBlockCondition")]
    public class IsBlockConditionSO : StateConditionSO<IsBlockCondition>
    {
        public PlayerStateInfoSO playerStateSO => _playerStateSO;

        [SerializeField] PlayerStateInfoSO _playerStateSO;
        protected override Condition CreateCondition() => new IsBlockCondition();
    }

    public class IsBlockCondition : Condition
    {
        private IsBlockConditionSO originSO => (IsBlockConditionSO)base.OriginSO;

        MonsterActor monster;

        public override void Awake(StateMachine _stateMachine)
        {
            monster = _stateMachine.GetComponent<MonsterActor>();
        }


        protected override bool Statement()
        {
            bool _isAttack = originSO.playerStateSO.IsAttack();
            return _isAttack;
        }
    }
}

