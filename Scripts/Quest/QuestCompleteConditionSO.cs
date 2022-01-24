using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    //[CreateAssetMenu(fileName = "QuestCompleteConditionSO", menuName = "Quest/QuestCompleteCondition")]
    public abstract class QuestCompleteConditionSO : ScriptableObject
    {
        internal int currentGoalNum; //��ǥ �޼� Ƚ��
        [SerializeField] internal int goalNum; //�޼��ؾ��ϴ� ��ǥ
        public virtual void Awake()
        {

        }
        
        
  


        public virtual void Exit()
        {

        }

        public abstract bool CompleteCondition();

    }
}

