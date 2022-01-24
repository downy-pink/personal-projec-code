using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerBlockhitActionSO", menuName = "State Machines/Actions/PlayerBlockhitAction")]
    public class PlayerBlockhitActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new PlayerBlockhitAction();
    }

    public class PlayerBlockhitAction : StateAction
    {
        private new PlayerBlockhitActionSO originSO => (PlayerBlockhitActionSO)base.OriginSO;

        Actor actor;
        Attack attack;

        public override void Awake(StateMachine _sm)
        {
            actor = _sm.GetComponent<Actor>();
            attack = _sm.GetComponent<Attack>();
        }

        public override void OnEnter()
        {
            attack.ColDisabled();
        }

        public override void OnExit()
        {
            actor.SetIsBlockhit(false);
            
        }

    }
}

