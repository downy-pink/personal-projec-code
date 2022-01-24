using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Quests
{
    [CreateAssetMenu(fileName = "UsedPotionQuestCompleteComditionSO", menuName = "Quest/UsedPotionQuestCompleteComdition")]

    //물약을 사용했는가 체크
    public class UsedPotionQuestCompleteComditionSO : QuestCompleteConditionSO
    {
        [SerializeField] GameEvents gameEventsSO; //게임 내 일어나는 이벤트들

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

        //게임오브젝트를 가져와 만들자.
        public override bool CompleteCondition() =>  currentGoalNum >= goalNum;

        void OnUsedPotion() => currentGoalNum++;
    }
}

