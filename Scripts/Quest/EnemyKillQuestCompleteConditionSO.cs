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
            Debug.Log("�������̴� �����ũ");
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

        //���ӿ�����Ʈ�� ������ ������.
        public override bool CompleteCondition()
        {
             return currentGoalNum >= goalNum;
        }

        void OnMonsterKill()
        {
            currentGoalNum++;
            Debug.Log("���� ���μ� " + currentGoalNum);
        }
    }
}

