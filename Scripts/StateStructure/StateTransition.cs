using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    public class StateTransition : IStateFlow
    {
        private StateCondition[] conditions; //taget으로 이동위한 조건
      
        internal StateTransition() { }
        public StateTransition(StateCondition[] conditions)
        {
            Init(conditions);
        }

        internal void Init(StateCondition[] _conditions)
        {
            conditions = _conditions;
        }

        public void OnEnter()
        {
            for (int i = 0; i < conditions.Length; ++i)
                conditions[i].condition.OnEnter();

        }

        public void OnExit()
        {
            for (int i = 0; i < conditions.Length; ++i)
                conditions[i].condition.OnExit();

        }

        internal bool IsCondition(bool _isEndOrMode)
        {
            if (_isEndOrMode)
            {
                for (int i = 0; i < conditions.Length; ++i)
                {
                    if (conditions[i].IsMet())
                        return true;
                }
                return false;
            }

            else
            {
                bool _isCondition = false;
                int _truenum = 0;
                for (int i = 0; i < conditions.Length; ++i)
                {
                    if (conditions[i].IsMet())
                        _truenum++;
                }
                if (_truenum == conditions.Length)
                    return true;

                return false;
            }
           
        
        }

    }
}

