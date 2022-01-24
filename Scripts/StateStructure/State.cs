using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    //Action을 모아서 State를 만드는 클래스
    public class State
    {
        internal string stateName;

        internal StateSO originSO;
        internal StateMachine stateMachine;
        internal StateAction[] actions;
        internal StateTransition startTransitions;
        internal StateTransition endTransitions;
        internal StateSO[] blockStatesSO;
        internal StateSO[] stopStatesSO;

        internal string[] blockStateName;
        internal string[] stopStateName;

        internal bool isEndOrMode; //탈출조건은 or아니면 And로 할것인가

        public void OnEnter()
        {
            if(actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                    actions[i].OnEnter();
            }


            startTransitions.OnEnter();
            endTransitions.OnEnter();
        }

        public void OnUpdate()
        {
            if(actions != null)
            {
                for(int i = 0; i < actions.Length; i++)
                    actions[i].OnUpdate();
            }

        }

        public void OnFixedUpdate()
        {
            if(actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                    actions[i].OnFixedUpdate();
            }

        }
       
        public void OnExit()
        {
            if(actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                    actions[i].OnExit();
            }

            if(startTransitions != null)
            startTransitions.OnExit();
            if (endTransitions != null)
                endTransitions.OnExit();
        }

        public StateSO GetStateSO() => originSO;

        public bool IsEndCondition()
        {
            if(endTransitions != null)
            return IsEndCondition(endTransitions);

            return false;
        }

        public bool IsStartCondition()
        {
            return IsStartCondition(startTransitions);
        }

        bool IsEndCondition(StateTransition _transition)
        {
            return _transition.IsCondition(isEndOrMode);
        }

        bool IsStartCondition(StateTransition _transition)
        {
          
     
            bool _isCondition = false;
     
            if (_transition.IsCondition(false))
                _isCondition = true;
            else
                _isCondition = false;
     
            return _isCondition;
     
     
        }

    }
}

