using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core;
using Quests;

namespace UI
{
    public class UIQuest : MonoBehaviour
    {
        [SerializeField] QuestManagerSO questMangerSO;
        [SerializeField] TextMeshProUGUI goalText;

        // Start is called before the first frame update
        void Start()
        {
            goalText.text = "도움이 필요한 마을사람을 찾자.";
        }

        // Update is called once per frame
        void Update()
        {
            OnGoalTextUpdate();
        }

        void OnGoalTextUpdate()
        {
            if(questMangerSO.GetCurrentQuest() != null)
            goalText.text = questMangerSO.GetCurrentQuest().GetName() + " ("+questMangerSO.GetCurrentQuest().GetQuestCurrentGoalNum().ToString()
               +" / " + questMangerSO.GetCurrentQuest().GetQuestGoalNum() + ")";

            if (questMangerSO.GetCurrentQuestState() == QuestState.COMPLETE)
                goalText.text = "임무 완료";

            if (questMangerSO.GetCurrentQuestState() == QuestState.NONE)
                goalText.text = "도움이 필요한 사람이 있다면 도와주세요.";

        }
    }
}

