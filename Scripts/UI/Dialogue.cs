using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Quests;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] GameEvents gameEventsSO;
        [SerializeField] UIEventsSO uiEventsSO;
        [SerializeField] GameObject dialogue;
        [SerializeField] GameObject questButton;
        [SerializeField] GameObject completeObj;
        [SerializeField] QuestManagerSO questManagerSO;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Button acceptButton;
        [SerializeField] Button refuseButton;
        [SerializeField] Button completeButton;
 
        int questID = -1;
        int dialogueIndex; //��ȭ������ �ε����� ����

        private void Start()
        {
            uiEventsSO.dialogueInteractionEvents += OnDialogue;
            acceptButton.onClick.AddListener(() => Accept());
            refuseButton.onClick.AddListener(() => Refuse());
            completeButton.onClick.AddListener(() => Complete());

            dialogue.SetActive(false);
            questButton.SetActive(false);
            completeObj.SetActive(false);
        }

        void Complete()
        {
            questManagerSO.GetCurrentQuest().OnReward();
            questManagerSO.GetquestLineDic()[questID].IncreaseCurrentQuestIndex();
            if (questID != -1)
                questManagerSO.RemoveQuest();
            dialogueIndex = 0;
            dialogue.SetActive(false);
            completeObj.SetActive(false);
            gameEventsSO.OnConversationExitEvent();
        }

        void Accept()
        {
            Debug.Log("������ư");
            if (questID != -1)
                questManagerSO.QuestEnroll(questManagerSO.GetquestLineDic()[questID].GetCurrentQuest());
            dialogueIndex = 0;
            dialogue.SetActive(false);
            questButton.SetActive(false);
            gameEventsSO.OnConversationExitEvent();
        }

        void Refuse()
        {
            dialogueIndex = 0;
            dialogue.SetActive(false);
            questButton.SetActive(false);
            gameEventsSO.OnConversationExitEvent();
        }


        void OnDialogue(int id)
        {

            questID = id;
                switch (questManagerSO.GetCurrentQuestState())
                {
                    case QuestState.NONE:
               
                    //ó�� ��ȭâ ����
                    if (dialogueIndex == 0)
                    {
                        gameEventsSO.OnConversationEvent();
                        dialogue.SetActive(true);
                    }
                       

                    //��ȭ����
                    if (questManagerSO.GetquestLineDic()[id].GetCurrentQuest().GetContents().Length > dialogueIndex)
                        text.text = questManagerSO.GetquestLineDic()[id].GetCurrentQuest().GetContents()[dialogueIndex++];
                    //��ȭ�� ������
                    else
                    questButton.SetActive(true);
                        break;

                    case QuestState.ACCEPT:

                    if (dialogueIndex == 0)
                    {
                        gameEventsSO.OnConversationEvent(); //��ȭ �߿� ���ؾ��� �÷��̾� �ൿ
                        dialogue.SetActive(true);
                    }

                    if (questManagerSO.GetCurrentQuest().GetProceedContents().Length > dialogueIndex)
                        text.text = questManagerSO.GetCurrentQuest().GetProceedContents()[dialogueIndex++];
                    else
                    {
                        dialogueIndex = 0;
                        gameEventsSO.OnConversationExitEvent();
                        dialogue.SetActive(false);
                    }
                    break;

                    case QuestState.COMPLETE:
                    if (dialogueIndex == 0)
                    {
                        gameEventsSO.OnConversationEvent();
                        dialogue.SetActive(true);
                    }

                    if (questManagerSO.GetCurrentQuest().GetCompleteContents().Length > dialogueIndex)
                        text.text = questManagerSO.GetCurrentQuest().GetCompleteContents()[dialogueIndex++];
                    else
                    {
                        //���� ������ ����Ʈ������ ������
                        if (questManagerSO.GetquestLineDic()[id].GetCurrentQuestIndex() < questManagerSO.GetquestLineDic()[id].GetQuestLineLength() - 1)
                        {
                            completeObj.SetActive(true);
                        }
                        else if (questManagerSO.GetquestLineDic()[id].GetCurrentQuestIndex() >= questManagerSO.GetquestLineDic()[id].GetQuestLineLength() - 1)
                        {
                            dialogueIndex = 0;
                            gameEventsSO.OnConversationExitEvent();
                            dialogue.SetActive(false);
                        }
                    }
                    break;
                }

        }


    }
}

