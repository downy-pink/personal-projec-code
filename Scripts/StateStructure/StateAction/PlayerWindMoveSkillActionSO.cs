using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Helper;
using Core;
using Quests;
using DG.Tweening;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerWindMoveSkillActionSO", menuName = "State Machines/Actions/PlayerWindMoveSkillAction")]
    public class PlayerWindMoveSkillActionSO : StateActionSO
    {
        public GameEvents gameEvents => _gameEvents;
        public UIEventsSO uiEventsSO => _uiEventsSO;

        [SerializeField] GameEvents _gameEvents;
        [SerializeField] UIEventsSO _uiEventsSO;
        protected override StateAction CreateAction() => new PlayerWindMoveSkillAction();
    }

    public class PlayerWindMoveSkillAction : StateAction
    {
        private new PlayerWindMoveSkillActionSO originSO => (PlayerWindMoveSkillActionSO)base.OriginSO;

        PlayerActor player;
        CapsuleCollider col;
        BoxCollider animationBodycol;
        Attack attack;
        public override void Awake(StateMachine sm)
        {
            player = sm.GetComponent<PlayerActor>();
            col = sm.GetComponent<CapsuleCollider>();
            animationBodycol = player.GetAnimationBodyCol();
            attack = sm.GetComponentInChildren<Attack>();
            attack.colDeactive += () => animationBodycol.enabled = false;
        }

        public override void OnEnter()
        {
            col.enabled = false;
            originSO.gameEvents.OnUsedSkillEvent();
            player.TryCameraShake(0.9f, 0.2f, 7.5f, 120);
            originSO.uiEventsSO.OnWindMoveCoolTimeEvent(4.1f);
        }
        
        float currentTime;
        bool isFlag;
        bool isFlag2;
        public override void OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 0.75f && !isFlag)
            {
                animationBodycol.enabled = true;
                Vector3 moveSpeed = (-player.modelRoot.up*65f + player.modelRoot.forward*35f);
                player.AddMomentum(moveSpeed);
                isFlag = true;
                player.ActiveWindMoveEffect();

            }

            if (currentTime >= 1.5f && !isFlag2)
            {
                player.DeactiveWindMoveEffect();
                isFlag2 = true;
            }
        }
        public override void OnExit()
        {
            isFlag = false;
            isFlag2 = false;
            currentTime = 0;
            col.enabled = true;
            animationBodycol.enabled = false;
           
        }


    }
}
