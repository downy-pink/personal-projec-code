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
        //���۷� ������ ~%�� ����
        [SerializeField] float attackSendSensitivity;
        bool isAttackFlag; //�������� ���� �� 1ȸ�� ����
        public bool IsAttack()
        {
            List<State> _states = playerStateM.GetCurrentStates();

            bool _isAttack = false;
            int _random = Random.Range(0, 1000); //ǥ��Ȯ���� ���� 1000���� Ȯ��
            if (playerStateM != null && _states != null && attackStateSO != null)
            {
                _isAttack = EqualStateSO(_states, attackStateSO); //���� �÷��̾��� ���°� �������ΰ�?
                if (_isAttack )
                {
                    if (attackSendSensitivity * 10 >= _random && !isAttackFlag)
                    {
                        return true;
                    }
                    isAttackFlag = true; //�� �ѹ��� IsAttack�Լ��� ����ǵ���
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

