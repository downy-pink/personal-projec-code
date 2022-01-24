using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerSkillActionSO", menuName = "State Machines/Actions/PlayerSkillAction")]
    public class PlayerSkillActionSO : StateActionSO
    {
        public GameEvents gameEventsSO => _gameEventSO;
        public UIEventsSO uiEventsSO => _uiEventsSO;

        [SerializeField] GameEvents _gameEventSO;
        [SerializeField] UIEventsSO _uiEventsSO;
        protected override StateAction CreateAction() => new PlayerSkillAction();
    }

    public class PlayerSkillAction : StateAction
    {
        private new PlayerSkillActionSO originSO => (PlayerSkillActionSO)base.OriginSO;
        PlayerActor player;

        public override void Awake(StateMachine sm)
        {
            player = sm.GetComponent<PlayerActor>();
        }

        public override void OnEnter()
        {
            originSO.gameEventsSO.OnUsedSkillEvent();
            player.ActiveSwordRTrail();
            player.ActiveSwordLTrail();
            player.StartCoroutine(DoSwordAura());
            player.TryCameraShake(1.1f, 0.2f, 7.5f, 120);
            player.TryCameraShake(2f, 0.2f, 7.5f, 120);
            originSO.uiEventsSO.OnSwordSkillCoolTimeEvent(7.1f);
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            player.DeactiveSwordRTrail();
            player.DeactiveSwordLTrail();
        }

        IEnumerator DoSwordAura()
        {
            player.ActiveSwordSkillAura();
            yield return new WaitForSeconds(6);
            player.DeactiveSwordSkillAura();
        }
    }
}

