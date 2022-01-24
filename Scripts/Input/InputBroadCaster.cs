using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace input
{
    [CreateAssetMenu(fileName = "InputBroadCaster", menuName = "Game/Input BroadCaster")]

    //입력의 중계자역할
    public class InputBroadCaster : ScriptableObject
    {
        public event UnityAction<Vector2> cameraMoveEvent = delegate { };
        public event UnityAction<Vector2> moveEvent = delegate { };
        public event UnityAction<bool> jumpEvent = delegate { };
        public event UnityAction<bool> attackEvent = delegate { };
        public event UnityAction<bool> mouseRightClickEvent = delegate { };
        public event UnityAction<bool> evasionEvent = delegate { };
        public event UnityAction<bool> qInputEvent = delegate { };
        public event UnityAction<bool> eInputEvent = delegate { };
        public event UnityAction<bool> rInputEvent = delegate { };

        public event UnityAction interactionEvent = delegate { };

        public void OnRInput(bool _isRInput)
        {
            if (rInputEvent != null)
                rInputEvent.Invoke(_isRInput);
        }

        public void OnEInput(bool _isQInput)
        {
            if (eInputEvent != null)
                eInputEvent.Invoke(_isQInput);
        }

        public void OnQInput(bool _isQInput)
        {
            if (qInputEvent != null)
                qInputEvent.Invoke(_isQInput);
        }

        public void OnInterAction()
        {
            if(interactionEvent != null)
            interactionEvent.Invoke();
        }

        public void OnEvasionKey(bool _isEvasionKey)
        {
            if (evasionEvent != null)
                evasionEvent.Invoke(_isEvasionKey);
        }

        public void OnRightMouseKey(bool _isRightClick)
        {
            if (mouseRightClickEvent != null)
                mouseRightClickEvent.Invoke(_isRightClick);
        }

        public void OnRotateCamera(Vector2 _inputValue)
        {
            if (cameraMoveEvent != null)
                cameraMoveEvent.Invoke(_inputValue);
        }

        public void OnMove(Vector2 _inputValue)
        {
            if (moveEvent != null)
                moveEvent.Invoke(_inputValue);
        }

        public void OnJump(bool _isjump)
        {
            if (jumpEvent != null)
                jumpEvent.Invoke(_isjump);
        }

        public void OnAttack(bool _isAttack)
        {
            if (attackEvent != null)
                attackEvent.Invoke(_isAttack);
        }
    }
}

