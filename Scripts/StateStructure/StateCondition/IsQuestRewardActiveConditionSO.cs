using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "IsQuestRewardActiveConditionSO", menuName = "State Machines/Conditions/IsQuestRewardActiveCondition")]
    public class IsQuestRewardActiveConditionSO : StateConditionSO<IsQuestRewardActiveCondition>
    {
        public QuestSO questSO => _questSO;

        [SerializeField] QuestSO _questSO;
        protected override Condition CreateCondition() => new IsQuestRewardActiveCondition();
    }

    public class IsQuestRewardActiveCondition : Condition
    {
        private IsQuestRewardActiveConditionSO originSO => (IsQuestRewardActiveConditionSO)base.OriginSO; // The SO this Condition spawned from

        bool isActive;

        public override void Awake(StateMachine _stateMachine)
        {
            originSO.questSO.rewardEvent += () => isActive = true;
            //isActive = true;
        }

        protected override bool Statement() => isActive;
    }
}

