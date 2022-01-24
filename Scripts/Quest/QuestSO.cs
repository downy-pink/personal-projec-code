using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Quests
{
    //퀘스트 정보를 기입
    [CreateAssetMenu(fileName = "QuestSO", menuName = "Quest/Quest")]
    public class QuestSO : ScriptableObject
    {
        public string name => _name;
        [SerializeField] string _name;

        public string[] contents => _contents;
        [SerializeField] string[] _contents;

        public string[] proceedingContents => _proceedingContents; //수락 후 NPC대화 텍스트
        [SerializeField] string[] _proceedingContents;

        public string[] completeContents => _completeContents;
        [SerializeField] string[] _completeContents;

        public QuestCompleteConditionSO questCompleteConditionSO => _questCompleteConditionSO; //퀘스트 완료 조건
        [SerializeField] QuestCompleteConditionSO _questCompleteConditionSO;

        public UnityAction rewardEvent = delegate { }; //완료 시 보상으로 주어지는 행위

        public void OnReward()
        {
            if (rewardEvent != null)
                rewardEvent.Invoke();
        }

        public virtual void Awake()
        {
            _questCompleteConditionSO.Awake();
        }

        public virtual void Exit()
        {
            _questCompleteConditionSO.Exit();
        }
    }

}

