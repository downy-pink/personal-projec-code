using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Core;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerHitActionSO", menuName = "State Machines/Actions/PlayerHitAction")]
    public class PlayerHitActionSO : StateActionSO
    {
        protected override StateAction CreateAction() => new PlayerHitAction();
    }

    public class PlayerHitAction : StateAction
    {
        private new PlayerHitActionSO originSO => (PlayerHitActionSO)base.OriginSO;

        PlayerActor player;
        Attack attack;
        public override void Awake(StateMachine _sm)
        {
            player = _sm.GetComponent<PlayerActor>();
            attack = _sm.GetComponent<Attack>();
        }

        public override void OnEnter()
        {
            if(player.GetEnemyT() != null)
            {
                Vector3 _v = player.transform.position - player.GetEnemyT().position;
                player.AddMomentum(_v.normalized * 4f);
            }

        }

        public override void OnExit()
        {
            attack.ColDisabled();
        }
    }
}

