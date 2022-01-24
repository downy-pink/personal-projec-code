using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Quests
{
    [CreateAssetMenu(fileName = "BossKillQuestConditionSO", menuName = "Quest/BossKillQuestCondition")]
    public class BossKillQuestConditionSO : QuestCompleteConditionSO
    {
        [SerializeField] GameEvents gameEventsSO;

        public override void Awake()
        {
            currentGoalNum = 0;
            gameEventsSO.deathBossEvent -= OnMonsterKill;
            gameEventsSO.deathBossEvent += OnMonsterKill;
        }

        private void OnEnable()
        {
            currentGoalNum = 0;
            gameEventsSO.deathBossEvent -= OnMonsterKill;
        }

        public override void Exit()
        {
            gameEventsSO.deathBossEvent -= OnMonsterKill;
        }

        //게임오브젝트를 가져와 만들자.
        public override bool CompleteCondition()
        {
            return currentGoalNum >= goalNum;
        }


        void OnMonsterKill()
        {
                currentGoalNum++;
            Debug.Log(currentGoalNum);
        }
    }
}

