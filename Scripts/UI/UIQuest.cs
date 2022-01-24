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
            goalText.text = "������ �ʿ��� ��������� ã��.";
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
                goalText.text = "�ӹ� �Ϸ�";

            if (questMangerSO.GetCurrentQuestState() == QuestState.NONE)
                goalText.text = "������ �ʿ��� ����� �ִٸ� �����ּ���.";

        }
    }
}

