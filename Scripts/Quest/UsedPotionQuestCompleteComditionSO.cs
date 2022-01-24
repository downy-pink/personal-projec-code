using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Quests
{
    [CreateAssetMenu(fileName = "UsedPotionQuestCompleteComditionSO", menuName = "Quest/UsedPotionQuestCompleteComdition")]

    //������ ����ߴ°� üũ
    public class UsedPotionQuestCompleteComditionSO : QuestCompleteConditionSO
    {
        [SerializeField] GameEvents gameEventsSO; //���� �� �Ͼ�� �̺�Ʈ��

        public override void Awake()
        {
            base.Awake();
            currentGoalNum = 0;
            gameEventsSO.usedPotionEvent -= OnUsedPotion;
            gameEventsSO.usedPotionEvent += OnUsedPotion;
        }

        public override void Exit()
        {
            base.Exit();
            gameEventsSO.usedPotionEvent -= OnUsedPotion;
        }

        //���ӿ�����Ʈ�� ������ ������.
        public override bool CompleteCondition() =>  currentGoalNum >= goalNum;

        void OnUsedPotion() => currentGoalNum++;
    }
}

