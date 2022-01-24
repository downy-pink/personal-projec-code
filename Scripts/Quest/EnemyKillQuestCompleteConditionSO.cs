using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Quests
{
    [CreateAssetMenu(fileName = "QuestCompleteConditionSO", menuName = "Quest/QuestCompleteCondition")]
    public class EnemyKillQuestCompleteConditionSO : QuestCompleteConditionSO
    {
        [SerializeField] GameEvents gameEventsSO;

        public override void Awake()
        {
            currentGoalNum = 0;
            Debug.Log("몬스터죽이는 어웨이크");
            gameEventsSO.deathMonsterEvent -= OnMonsterKill;
            gameEventsSO.deathMonsterEvent += OnMonsterKill;
        }

        public override void Exit()
        {
            gameEventsSO.deathMonsterEvent -= OnMonsterKill;
        }

        private void OnEnable()
        {
            currentGoalNum = 0;
            gameEventsSO.deathMonsterEvent -= OnMonsterKill;
        }

        //게임오브젝트를 가져와 만들자.
        public override bool CompleteCondition()
        {
             return currentGoalNum >= goalNum;
        }

        void OnMonsterKill()
        {
            currentGoalNum++;
            Debug.Log("몬스터 죽인수 " + currentGoalNum);
        }
    }
}

