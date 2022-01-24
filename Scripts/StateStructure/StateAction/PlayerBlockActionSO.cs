using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerBlockActionSO", menuName = "State Machines/Actions/PlayerBlockAction")]
    public class PlayerBlockActionSO : StateActionSO
    {
        public float staminaConsume => _staminaConsume;

        [SerializeField] float _staminaConsume;
        protected override StateAction CreateAction() => new PlayerBlockAction();
    }

    public class PlayerBlockAction : StateAction
    {
        private new PlayerBlockActionSO originSO => (PlayerBlockActionSO)base.OriginSO;

        PlayerActor player;
        Stats stats;

        public override void Awake(StateMachine _sm)
        {
            player = _sm.GetComponent<PlayerActor>();
            stats = _sm.GetComponent<Stats>();
        }

        

        public override void OnEnter()
        {
            player.SetBlock(true);
        }

        public override void OnUpdate()
        {
            stats.staminaConsume(originSO.staminaConsume * Time.deltaTime);
            //Debug.Log(stats.GetStamina());
        }

        public override void OnExit()
        {
            player.SetBlock(false);
        }

    }
}

