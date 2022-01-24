using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actors;
using StateStructure;

namespace Actions
{
    public class PlayerMove : MonoBehaviour
    {
        PlayerActor actor;

        
        private void Start()
        {
            actor = GetComponent<PlayerActor>();
           
        }
        public void OnEnter()
        {

        }

        public void OnUpdate()
        {
           

        }

        public void OnFixedUpdate()
        {
            //Vector3 _velocity = new Vector3();
            //_velocity += Vector3.ProjectOnPlane(actor.GetCameraTransform().right, actor.transform.up).normalized * actor.GetmoveInputValue().x;
            //_velocity += Vector3.ProjectOnPlane(actor.GetCameraTransform().forward, actor.transform.up).normalized * actor.GetmoveInputValue().y;
            //actor.GetRigid().velocity += _velocity /** actor.GetSpeed()*/ * Time.deltaTime;
            ////if (actor.GetmoveInputValue() == Vector2.zero)
            ////    actor.GetSM().RemoveAction(this);
        }

        public void OnExit()
        {
            actor.GetRigid().velocity = Vector3.zero;
        }

    }
}

