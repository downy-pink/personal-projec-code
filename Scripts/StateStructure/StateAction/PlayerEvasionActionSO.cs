using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;


namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerEvasionActionSO", menuName = "State Machines/Actions/PlayerEvasionAction")]
    public class PlayerEvasionActionSO : StateActionSO
    {
        public float staminaConsume => _staminaConsume;
        public float addMomentum => _addmoMentum;

        [SerializeField] float _staminaConsume;
        [SerializeField] float _addmoMentum;
        protected override StateAction CreateAction() => new PlayerEvasionAction();
    }

    public class PlayerEvasionAction : StateAction
    {
        private new PlayerEvasionActionSO originSO => (PlayerEvasionActionSO)base.OriginSO;

        PlayerActor player;
        Stats stats;

        public override void Awake(StateMachine _sm)
        {
            player = _sm.GetComponent<PlayerActor>();
            stats = _sm.GetComponent<Stats>();
        }



        public override void OnEnter()
        {
            stats.staminaConsume(originSO.staminaConsume);
            player.ForwardAddMomentum(originSO.addMomentum);
            player.SetEvasion(true);
            player.ActiveEvasionEffect();
        }


        public override void OnExit()
        {
            player.SetEvasion(false);
            player.DeactiveEvasionEffect();
        }

    }
}

