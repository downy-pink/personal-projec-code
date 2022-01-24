using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using DG.Tweening;
using Quests;

namespace UI
{
    public abstract class UISkill : MonoBehaviour
    {
        [SerializeField] QuestSO enemyKillQuest;
        [SerializeField] protected UIEventsSO uiEventsSO;
        [SerializeField] Image CooltimeImg;

         void Start()
        {
            CooltimeImg.fillAmount = 0;
            gameObject.SetActive(false);
            enemyKillQuest.rewardEvent += () => gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            EnrollSkillCoolTime();
        }

        private void OnDisable()
        {
            ReleaseSkillCoolTime();
        }


        public abstract void EnrollSkillCoolTime();

        public abstract void ReleaseSkillCoolTime();

        public void OnSkillCoolTime(float _coolTime)
        {
            CooltimeImg.fillAmount = 1;
            CooltimeImg.DOFillAmount(0, _coolTime);
        }


    }

}
