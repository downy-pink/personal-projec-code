using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Quests;
using Actors;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerUsedPotionActionSO", menuName = "State Machines/Actions/PlayerUsedPotionAction")]
    public class PlayerUsedPotionActionSO : StateActionSO
    {
        public int hpIncreaseNum => _hpIncreaseNum;
        public QuestSO questSO => _questSO;
        public UIEventsSO uiEventsSO => _uiEventsSO;

        [SerializeField] int _hpIncreaseNum;
        [SerializeField] QuestSO _questSO;
        [SerializeField] UIEventsSO _uiEventsSO;
        protected override StateAction CreateAction() => new PlayerUsedPotionAction();
    }

    public class PlayerUsedPotionAction : StateAction
    {
        private new PlayerUsedPotionActionSO originSO => (PlayerUsedPotionActionSO)base.OriginSO;

        Stats stats;
        PlayerActor player;

        public override void Awake(StateMachine _sm)
        {
            stats = _sm.GetComponent<Stats>();
            player = _sm.GetComponent<PlayerActor>();
            originSO.questSO.rewardEvent += () => isReward = true;
        }

        bool isReward;
        public override void OnEnter()
        {
            //isReward = true;
            if (isReward)
            {
                stats.InCreaseHP(originSO.hpIncreaseNum);
                player.StartCoroutine(DoPotionEatEffect());
                originSO.uiEventsSO.OnPotionCoolTime(5);
            }
            Debug.Log(stats.GetHP());
        }

        IEnumerator DoPotionEatEffect()
        {
            player.ActivepotionEatEffect();
            yield return new WaitForSeconds(1.1f);
            player.DeactivepotionEatEffect();
        }
    }
}

