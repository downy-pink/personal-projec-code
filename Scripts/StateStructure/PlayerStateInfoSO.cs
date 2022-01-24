using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using UnityEngine.Events;

namespace StateStructure
{
    [CreateAssetMenu(fileName = "PlayerStateInfoSO", menuName = "Helper/PlayerStateInfo")]
    public class PlayerStateInfoSO : ScriptableObject
    {
        internal StateMachine playerStateM;
        [SerializeField] StateSO attackStateSO;
        //전송률 공격의 ~%만 전송
        [SerializeField] float attackSendSensitivity;
        bool isAttackFlag; //공격전송 공격 시 1회만 실행
        public bool IsAttack()
        {
            List<State> _states = playerStateM.GetCurrentStates();

            bool _isAttack = false;
            int _random = Random.Range(0, 1000); //표본확장을 위해 1000까지 확장
            if (playerStateM != null && _states != null && attackStateSO != null)
            {
                _isAttack = EqualStateSO(_states, attackStateSO); //현재 플레이어의 상태가 공격중인가?
                if (_isAttack )
                {
                    if (attackSendSensitivity * 10 >= _random && !isAttackFlag)
                    {
                        return true;
                    }
                    isAttackFlag = true; //단 한번만 IsAttack함수가 실행되도록
                }
            }

            if (!_isAttack)
                isAttackFlag = false;
            return false;
        }

        public bool EqualStateSO(List<State> _states, StateSO _stateSO)
        {
            for(int i = 0; i < _states.Count; ++i)
            {
                if (_states[i].originSO == _stateSO)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

