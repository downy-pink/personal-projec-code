using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using Actions;


namespace StateStructure
{

    //모든 상태 관리, 상태를 업데이트
    public class StateMachine : MonoBehaviour
    {
        //플레이어
        List<State> currentStates; //현재 실행되는 상태
        List<State> allStates; //오브젝트가 가지고있는 모든상태

        //AI
        State currentState;
        [SerializeField] bool aiMode;

        [SerializeField] TransitionSO transitionSO = default; //TransitionSO에서 초기화된 State를 다 받아온다.

        private void Start()
        {
            if(aiMode)
                currentState = new State();
            else
            currentStates = new List<State>();

            allStates = new List<State>();
            allStates = transitionSO.GetInitialState(this);
            
        }



        public void SetCurrentState(State _state)
        {
            if(aiMode)
            {
                if (currentState == _state || !IsCanRunSate(_state))
                    return;
                if(currentState != null)
                currentState.OnExit();
                currentState = _state;
                _state.OnEnter();
            }

            //NoneAI
            else
            {
                if (currentStates.Contains(_state) || !IsCanRunSate(_state))
                {
                    return;
                }
                IsStopState(_state);
                currentStates.Add(_state);
                _state.OnEnter();
            }
    
        }

        public List<State> GetCurrentStates()
        {
            return currentStates;
        }

        public State GetCurrentState()
        {
            return currentState;
        }

        internal void RemoveState(State _state)
        {
            if(aiMode && currentState != null)
            {
                if(currentState == _state)
                {
                    currentState.OnExit();
                    currentState = null;
                }
            }

            else
            {
                if (currentStates.Contains(_state))
                {
                    _state.OnExit();
                    currentStates.Remove(_state);
                }
            }

        }

        private void Update()
        {
            if(aiMode)
            {
                if(currentState != null)
                {
                    if (currentState.IsEndCondition())
                    {
                        RemoveState(currentState);
                        return;
                    }
                    currentState.OnUpdate();
                }

                for (int i = 0; i < allStates.Count; ++i) 
                {
                    if (allStates[i] != currentState && allStates[i].IsStartCondition())
                    {
                        SetCurrentState(allStates[i]);
                    }
                }
            }

            else
            {
                for (int i = 0; i < currentStates.Count; ++i)
                {

                    if (currentStates[i].IsEndCondition())
                    {
                        RemoveState(currentStates[i]);
                        return;
                    }
                    currentStates[i].OnUpdate();
                    //Debug.Log(currentStates[i].actions[0].ToString());
                }

                for (int i = 0; i < allStates.Count; ++i)
                    if (allStates[i].IsStartCondition())
                        SetCurrentState(allStates[i]);
            }
        }


        private void FixedUpdate()
        {
            if(aiMode)
            {
                if(currentState != null)
                {
                    if (currentState.IsEndCondition())
                    {
                        RemoveState(currentState);
                        return;
                    }
                    currentState.OnFixedUpdate();
                }
            }

            else
            {
                for (int i = 0; i < currentStates.Count; ++i)
                {
                    if (currentStates[i].IsEndCondition())
                    {
                        RemoveState(currentStates[i]);
                        return;
                    }
                    currentStates[i].OnFixedUpdate();
                    //Debug.Log(currentStates[i].actions[0].ToString());
                }
                //GetComponent<PlayerActor>().SetVelocity(GetComponent<PlayerActor>().momentum + GetComponent<PlayerActor>().nonMomentVelocity);
            }

        }

        //상태에 진입해도 되는지를 묻는 함수
        bool IsCanRunSate(State _state)
        {
            if(aiMode)
            {
                if(currentState != null)
                {
                    for (int i = 0; i < _state.blockStatesSO.Length; ++i)
                    {
                        if (currentState.originSO == _state.blockStatesSO[i]) //서로 실행이 되면 안되는 상태가 실행중이면 false반환
                            return false;
                    }
                }

            }

            else
            {
                for (int i = 0; i < currentStates.Count; ++i)
                {
                    for (int j = 0; j < _state.blockStatesSO.Length; ++j)
                    {
                        if (currentStates[i].originSO == _state.blockStatesSO[j]) //서로 실행이 되면 안되는 상태가 실행중이면 false반환
                            return false;
                    }
                }
            }
   
            
            return true;
        }

        //상태진입 시 멈춰야하는 상태가 있다면 멈춰준다.
        void IsStopState(State _state)
        {
            if (currentStates != null && _state != null)
            {
                for (int i = 0; i < currentStates.Count; ++i)
                {
                    for (int j = 0; j < _state.stopStatesSO.Length; ++j)
                    {
                        if (currentStates[i].originSO == _state.stopStatesSO[j])
                        {
                            RemoveState(currentStates[i]);
                            break;
                        }
                    }
                }
            }
   
            //if (currentStates != null && _state != null)
            //{
            //    for (int i = 0; i < currentStates.Count; ++i)
            //    {
            //        for (int j = 0; j < _state.stopStatesSO.Length; ++j)
            //        {
            //            if (currentStates[i].stateName == _state.stopStateName[j] && currentStates.Count > 0 && _state.stopStatesSO.Length > 0)
            //            {
            //                RemoveState(currentStates[i]);

            //            }
            //        }
            //    }
            //}
        }

    }

}

