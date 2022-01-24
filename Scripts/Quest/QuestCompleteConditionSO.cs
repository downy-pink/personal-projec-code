using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    //[CreateAssetMenu(fileName = "QuestCompleteConditionSO", menuName = "Quest/QuestCompleteCondition")]
    public abstract class QuestCompleteConditionSO : ScriptableObject
    {
        internal int currentGoalNum; //목표 달성 횟수
        [SerializeField] internal int goalNum; //달성해야하는 목표
        public virtual void Awake()
        {

        }
        
        
  


        public virtual void Exit()
        {

        }

        public abstract bool CompleteCondition();

    }
}

