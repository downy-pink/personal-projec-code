using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateStructure
{
    public class StateAction : IStateFlow
    {

        internal StateActionSO originSO;

        protected StateActionSO OriginSO => originSO;

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnExit() { }

        public virtual void Awake(StateMachine sm) { }

        public enum SpecificMoment
        {
            OnEnter, OnExit, OnUpdate,
        }
    }

}
