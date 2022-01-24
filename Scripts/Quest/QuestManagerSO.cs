using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public enum QuestState { NONE, ACCEPT, COMPLETE}
    [CreateAssetMenu(fileName = "QuestManagerSO", menuName = "Quest/QuestManager")]
    //퀘스트 관리 및 등록
    public class QuestManagerSO : ScriptableObject
    {
        Quest currentQuest;
        Dictionary<int, QuestLine> questLineDic = new Dictionary<int, QuestLine>();
        QuestState currentQuestState;

        public Dictionary<int, QuestLine> GetquestLineDic()
        {
            return questLineDic;
        }

        public Quest GetCurrentQuest()
        {
            return currentQuest;
        }

        private void OnEnable()
        {
            if(currentQuest!=null)
            {
                currentQuest.OnAwake(); //초기화
                currentQuest = null;
            }
        }

        public QuestState GetCurrentQuestState()
        {
            if (currentQuest == null)
                currentQuestState = QuestState.NONE;
            else if (!currentQuest.IsComplete()
                && currentQuest != null)
                currentQuestState = QuestState.ACCEPT;
            else if (currentQuest.IsComplete()
                && currentQuest != null)
                currentQuestState = QuestState.COMPLETE;

            return currentQuestState;
        }

        public void AddquestLineDic(QuestLine _qL)
        {
            questLineDic.Add(_qL.id, _qL);
        }

        public void RemoveQuest()
        {
            currentQuest = null;
        }
        
        public void QuestEnroll(Quest _quest)
        {
            currentQuest = _quest;
            currentQuest.OnAwake(); //초기화
        }
    }
}

