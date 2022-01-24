using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Quests
{
    [CreateAssetMenu(fileName = "EnemySkillKillQuestCompleteConditionSO", menuName = "Quest/EnemySkillKillQuestCompleteCondition")]
    public class EnemySkillKillQuestCompleteConditionSO : QuestCompleteConditionSO
    {
        [SerializeField] GameEvents gameEventsSO;
        bool isSkill;

        public override void Awake()
        {
            isSkill = false;
            currentGoalNum = 0;
            gameEventsSO.deathMonsterEvent -= OnMonsterKill;
            gameEventsSO.usedSkillEvent -= OnUsedSkill;

            gameEventsSO.deathMonsterEvent += OnMonsterKill;
            gameEventsSO.usedSkillEvent += OnUsedSkill;
        }

        public override void Exit()
        {
            gameEventsSO.deathMonsterEvent -= OnMonsterKill;
            gameEventsSO.usedSkillEvent -= OnUsedSkill;
        }

        private void OnEnable()
        {
            isSkill = false;
            currentGoalNum = 0;
            gameEventsSO.deathMonsterEvent -= OnMonsterKill;
            gameEventsSO.usedSkillEvent -= OnUsedSkill;
        }



        //게임오브젝트를 가져와 만들자.
        public override bool CompleteCondition()
        {
            return currentGoalNum >= goalNum;
        }

        void OnUsedSkill() => isSkill = true;

        void OnMonsterKill()
        {
            if (isSkill)
                currentGoalNum++;
            else
                isSkill = false;
            Debug.Log(currentGoalNum);
        }
    }
}

