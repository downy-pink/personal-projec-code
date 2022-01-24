using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Quests
{
    //NPC�� ����Ʈ���� ����
    public class Quest
    {
        string name;

        string[] contents;

        string[] proceedingContents;

        string[] completeContents;

        QuestCompleteConditionSO questCompleteConditionSO;

        bool isComplete;

        UnityAction awakeEvent;
        UnityAction rewardEvent;

        public string[] GetContents()
        {
            return contents;
        }

        public string[] GetProceedContents()
        {
            return proceedingContents;
        }

        public string[] GetCompleteContents()
        {
            return completeContents;
        }

        public string GetName()
        {
            return name;
        }

        public int GetQuestGoalNum() => questCompleteConditionSO.goalNum;
        public int GetQuestCurrentGoalNum() => questCompleteConditionSO.currentGoalNum;

        public bool IsComplete()
        {
            return isComplete = questCompleteConditionSO.CompleteCondition();
        }
        public Quest(QuestSO _questSO)
        {
            name = _questSO.name;
            contents = _questSO.contents;
            proceedingContents = _questSO.proceedingContents;
            completeContents = _questSO.completeContents;
            questCompleteConditionSO = _questSO.questCompleteConditionSO;
            awakeEvent = _questSO.Awake;
            rewardEvent = _questSO.OnReward;
        }

        public void OnReward()
        {
            if (rewardEvent != null)
                rewardEvent.Invoke();
        }

        public void OnAwake()
        {
            if (awakeEvent != null)
                awakeEvent.Invoke();
        }
    }
    public class QuestLine : MonoBehaviour
    {
        public int id; //NPC�����ϱ� ����
        [SerializeField] List<QuestSO> questLineSO;
        List<Quest> questLine;
        int currentQeustIndex; //���� �������� ����Ʈ
        [SerializeField] QuestManagerSO questManagerSO;

        [SerializeField] GameObject noneQuestEffect;
        [SerializeField] GameObject acceptQuestEffect;
        [SerializeField] GameObject compleQuestEffect;



        // Start is called before the first frame update
        void Start()
        {
               //����ƮSO������ ��������
               questLine = new List<Quest>();
            for(int i = 0; i < questLineSO.Count; ++i)
            {
                Quest _q = new Quest(questLineSO[i]);
                questLine.Add(_q);
            }
            questManagerSO.GetquestLineDic().Add(id, this);

            noneQuestEffect.active = false;
            acceptQuestEffect.active = false;
            compleQuestEffect.active = false;
        }

        private void Update()
        {
            questStateEffect();
        }

        void questStateEffect()
        {
            //����Ʈ�� �����ʾ����� ���� ����Ʈ�� �ִ�.������
            if (questManagerSO.GetCurrentQuestState() == QuestState.NONE &&
               currentQeustIndex < questLine.Count)
            {
                noneQuestEffect.active = true;
                acceptQuestEffect.active = false;
                compleQuestEffect.active = false;
            }

            //���
            else if (questManagerSO.GetCurrentQuestState() == QuestState.ACCEPT)
            {
                noneQuestEffect.active = false;
                acceptQuestEffect.active = true;
                compleQuestEffect.active = false;
            }

            //���
            else if (questManagerSO.GetCurrentQuestState() == QuestState.COMPLETE)
            {
                noneQuestEffect.active = false;
                acceptQuestEffect.active = false;
                compleQuestEffect.active = true;
            }

            else
            {
                noneQuestEffect.active = false;
                acceptQuestEffect.active = false;
                compleQuestEffect.active = false;
            }
        }

        public void IncreaseCurrentQuestIndex() => currentQeustIndex++; 

        public int GetCurrentQuestIndex() => currentQeustIndex;


        public int GetQuestLineLength() => questLineSO.Count;

        public Quest GetCurrentQuest() => questLine[currentQeustIndex];

    }
}

