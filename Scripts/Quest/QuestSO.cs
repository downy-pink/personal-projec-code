using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Quests
{
    //����Ʈ ������ ����
    [CreateAssetMenu(fileName = "QuestSO", menuName = "Quest/Quest")]
    public class QuestSO : ScriptableObject
    {
        public string name => _name;
        [SerializeField] string _name;

        public string[] contents => _contents;
        [SerializeField] string[] _contents;

        public string[] proceedingContents => _proceedingContents; //���� �� NPC��ȭ �ؽ�Ʈ
        [SerializeField] string[] _proceedingContents;

        public string[] completeContents => _completeContents;
        [SerializeField] string[] _completeContents;

        public QuestCompleteConditionSO questCompleteConditionSO => _questCompleteConditionSO; //����Ʈ �Ϸ� ����
        [SerializeField] QuestCompleteConditionSO _questCompleteConditionSO;

        public UnityAction rewardEvent = delegate { }; //�Ϸ� �� �������� �־����� ����

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

